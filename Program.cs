using System.IO;
using System.Text;

public class UpdateFileCreationTime
{
    public static void Main(string[] args)
    {
        string directoryPath;
        switch (args.Length)
        {
            // if they don't enter anything. Assume current directory and random datetime
            case 0:
                Console.WriteLine("This Utility recursively changes the datetime for access, lastwrite and creation on files and folders");
                Console.WriteLine("First off, enter a path to start from. All subdirectories and files will have their times altered");
                string path = Console.ReadLine();

                if (path == string.Empty)
                {
                   path = Directory.GetCurrentDirectory();
                }

                Console.WriteLine("Then enter a number between 0 and 700000000. This sets the time in ticks past 2001 until today");
                string offset = Console.ReadLine();

                if (offset == string.Empty)
                {
                    Random random = new Random();
                    offset = random.Next(0, 700000000).ToString();
                }

                Directory.SetCurrentDirectory(path);
                directoryPath = Directory.GetCurrentDirectory();
                UpdateCreationTime(directoryPath, Convert.ToInt32(offset));
                break;

                // if there's one argument, assume it's current folder and they're setting the time.
            case 1:
                directoryPath = Directory.GetCurrentDirectory();
                UpdateCreationTime(directoryPath, Convert.ToInt32(args[0]));
                break;

                //if there's 2 then do what they say
            case 2:
                Directory.SetCurrentDirectory(args[0]);
                directoryPath = Directory.GetCurrentDirectory();
                UpdateCreationTime(directoryPath, Convert.ToInt32(args[1]));
                break;

            default:
                break;
        }
    }

    public static DateTime UnixTimeStampToDateTime(double time)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(time).ToLocalTime();
        return dateTime;
    }

    
    private static void UpdateCreationTime(string directoryPath, Int32 offset)
    {
        string[] directoryPaths = Directory.GetDirectories(directoryPath);
        int i = 0;
        int k = 0;
        try
        {
            for (; i < Directory.GetDirectories(directoryPath).Length; i++)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPaths[i]);
                if (directoryInfo.Attributes.HasFlag(FileAttributes.ReadOnly))
                { i++; }
                directoryInfo.CreationTime = UnixTimeStampToDateTime(777777777 + offset);
                directoryInfo.LastWriteTime = UnixTimeStampToDateTime(888888888 + offset);
                directoryInfo.LastAccessTime = UnixTimeStampToDateTime(999999999 + offset);

                UpdateCreationTime(directoryPaths[i].ToString(), offset);
                Console.WriteLine("Updating files in: " + directoryPath + " file count is:" + (Directory.GetFiles(directoryInfo.FullName, "*.*", SearchOption.TopDirectoryOnly).Length));
            }

            string[] filePaths = Directory.GetFiles(directoryPath);

            for (; k < filePaths.Length; k++)
            {
                FileInfo fileInfo = new FileInfo(filePaths[k]);
                if (fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly))
                { k++; }
                fileInfo.CreationTime = UnixTimeStampToDateTime(999999999 + offset);
                fileInfo.LastWriteTime = UnixTimeStampToDateTime(999999999 + offset);
                fileInfo.LastAccessTime = UnixTimeStampToDateTime(999999999 + offset);
            }
        }
        catch (System.AccessViolationException f)
        {
            k++;
            Console.WriteLine(f.Message);
            UpdateCreationTime(directoryPaths[i].ToString(), offset);
        }
        catch (System.Exception e)
        {
            i++;
            Console.WriteLine(e.Message);
            UpdateCreationTime(directoryPaths[i].ToString(), offset);
        } 
    }
}


