using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#pragma warning disable
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

public class LogItem
{
    public string time;
    public string module;
    public string content;
}

public class LogConsumer
{
    //File to Store data finally
    private string mLogFileName = @"LogConsumer-" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt";
    private FileStream mLogFileStream;
    private StreamWriter mLogStreamWriter;

    //Queue to store logData indrectly
    private Queue<LogItem> mQueue = new Queue<LogItem>();
    private const int BUFFER_SIZE = 10;

    //Semaphore and Mutex
    private Semaphore fillCount = new Semaphore(0, BUFFER_SIZE);
    private Semaphore emptyCount = new Semaphore(BUFFER_SIZE, BUFFER_SIZE);
    private Mutex bufferMutex = new Mutex();

    //Consumer thread: write log
    private Thread consumerThread;

    //Singleton
    private static LogConsumer instance = new LogConsumer();
    public static LogConsumer Instance
    {
        get { return instance; }
        private set { }
    }

    private LogConsumer()
    {
        //Open file stream
        OpenFileStream();

        //Log consumer
        consumerThread = new Thread(Consumer);
        consumerThread.Start();
    }

    ~LogConsumer()
    {
        CloseFileStream();
    }

    /// <summary>
    /// Producer: Write the log
    /// Other modules also write log by following method
    /// </summary>
    /// <param name="content">Log content</param>
    public void Write(string content)
    {
        //Time of each log entry
        string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //Get the module that call this method
        StackTrace trace = new StackTrace();
        StackFrame frame = trace.GetFrame(1);
        MethodBase method = frame.GetMethod();
        string namespaceName = method.ReflectedType.Namespace;
        string className = method.ReflectedType.Name;
        string methodName = method.Name;
        string module = namespaceName + ":" + className + "." + methodName;

        //Production log entry
        var item = ProduceItem(time, module, content);

        //waiting for production Permission
        emptyCount.WaitOne();
        bufferMutex.WaitOne();

        //put the product into the buffer
        putItemIntoBuffer(item);
        bufferMutex.ReleaseMutex();

        //release one to take permission
        fillCount.Release();
    }

    //Open file stream
    private void OpenFileStream()
    {
        if(!string.IsNullOrEmpty(mLogFileName))//check if had routine
        {
            if (mLogFileStream == null)
                mLogFileStream = new FileStream(mLogFileName, FileMode.Append);
            if (mLogStreamWriter == null)
                mLogStreamWriter = new StreamWriter(mLogFileStream);//stream buffer??

        }
    }

    //Close file stream
    private void CloseFileStream()
    {
        if (mLogStreamWriter != null)//following operation is similar with the single buffer/queue
        {
            mLogStreamWriter.Flush();//this is a writer buffer, need to clear
            mLogStreamWriter.Close();
            mLogStreamWriter = null;
        }
        if (mLogFileStream != null)
        {
            mLogFileStream.Close();
            mLogFileStream = null;
        }
    }

    //real consumer
    private void Consumer()
    {
        while(true)
        {
            //wait for a permission
            fillCount.WaitOne();
            bufferMutex.WaitOne();

            //remove an item
            var item = removeItemFromBuffer();
            bufferMutex.ReleaseMutex();

            //release a production permission
            emptyCount.Release();

            //Consumption: write a log
            WriteLog(item);

        }
    }

    //put the product in the cache
    private void putItemIntoBuffer(LogItem item)
    {
        mQueue.Enqueue(item);
    }

    //remove the product from buffer
    private LogItem removeItemFromBuffer()
    {
        var item = mQueue.Peek();
        mQueue.Dequeue();
        return item;
    }

    //produce item
    private LogItem ProduceItem(string logTime, string logModule, string logContent)
    {
        LogItem item = new LogItem() { time = logTime, module = logModule, content = logContent };
        return item;
    }

    //Write log to file
    private void WriteLog(LogItem logItem)
    {
        if (mLogStreamWriter == null)
            OpenFileStream();
        mLogStreamWriter.WriteLine(logItem.time + " " +
            logItem.module + " " + logItem.content);//write log to the buffer
        mLogStreamWriter.Flush();//clear writer buffer
    }
}



public class GameLogger
{
    private Queue<string> queue = new Queue<string>();
    private string[] Str = { "asd", "ssss", "weqeq" };
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("Test", 0, 2);
        
    }

    public void Test()
    {
        newActivity(Str[Random.Range(0, Str.Length)]);
    }

    string text = "";

    public void newActivity(string activity)
    {
        if(queue.Count >= 3)
        {
            queue.Dequeue();
        }
        queue.Enqueue(activity);
        foreach(string ac in queue)
        {
            text += ac + "\n";
        }
    }

    private void onLogMessage(string condition, string stackTrace, LogType type)
    {
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 500, 300), text);
    }
    
}
