using System;
using System.IO;
using System.Numerics;

public class UpdateFileCreationTime
{
    public static void Main(string[] args)
    {
        string directoryPath;
        if (args.Length == 0)
        {
            directoryPath = Directory.GetCurrentDirectory();
        }
        else
        {
            directoryPath = args[0];
        }
        UpdateCreationTime(directoryPath);
        Console.WriteLine("File creation times updated in directory: " + directoryPath);
    }
    private static void UpdateCreationTime(string directoryPath)
    {
        try
        {
            foreach (string d in Directory.GetDirectories(directoryPath))
            {
                UpdateCreationTime(d);
            }
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                FileInfo fileInfo = new FileInfo(file);
                fileInfo.CreationTime = DateTime.Now;
                fileInfo.LastWriteTime = DateTime.Now;
                fileInfo.LastAccessTime = DateTime.Now;
                Console.WriteLine("Updated time for file: " + fileInfo.FullName);
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine("eeroooorrrr");
        }
    
    }
}


