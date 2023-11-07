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
            directoryPath = Directory.GetParent(directoryPath).FullName;
        }
        else
        {
           directoryPath = args[0];
        }
        UpdateCreationTime(directoryPath);
        Console.WriteLine("File creation times updated in directory: " + directoryPath);
    }

    static void UpdateCreationTime(string directoryPath)
    {
        string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

        foreach (string file in files)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file);
                fileInfo.CreationTime = DateTime.Now;
                Console.WriteLine("Updated creation time for file: " + fileInfo.FullName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating creation time for file: " + file);
                Console.WriteLine(ex.Message);
            }
        }
    }
}


