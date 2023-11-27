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
        int i = 0;
        string[] paths = Directory.GetDirectories(directoryPath);
        try
        {
            for (; i < Directory.GetDirectories(directoryPath).Length; i++)
            {                
                DirectoryInfo directoryInfo = new DirectoryInfo(paths[i]);
                directoryInfo.CreationTime = UnixTimeStampToDateTime(777777777 + offset);
                directoryInfo.LastWriteTime = UnixTimeStampToDateTime(888888888 + offset);
                directoryInfo.LastAccessTime = UnixTimeStampToDateTime(999999999 + offset);
                Console.WriteLine("Updated Directory Info for Directory: " + directoryInfo.FullName);
                UpdateCreationTime(paths[i].ToString(), offset);                             
            }

            foreach (var file in Directory.GetFiles(directoryPath))
            {
                FileInfo fileInfo = new FileInfo(file);
                fileInfo.IsReadOnly = false;
                fileInfo.CreationTime = UnixTimeStampToDateTime(999999999 + offset);
                fileInfo.LastWriteTime = UnixTimeStampToDateTime(999999999 + offset);
                fileInfo.LastAccessTime = UnixTimeStampToDateTime(999999999 + offset);
                Console.WriteLine("Updating files in: " + directoryPath + " file count is:" + Directory.GetFiles(directoryPath).Length);
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
            i++;
            UpdateCreationTime(paths[i].ToString(), offset);
        }
    }
}


