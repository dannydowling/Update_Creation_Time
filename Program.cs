public class UpdateFileCreationTime
{
    public static void Main(string[] args)
    {
        Console.WriteLine("This Utility recursively changes the datetime for access, lastwrite and creation on all files and folders");
        Console.WriteLine(@"To use it enter a start path and an integer between 0 and 700000000 such as C:\Movies 69696969");
        Console.WriteLine("It's fairly safe to use. I've made it so that it only writes times after 2001, to stop year 2000 weirdness");

        string directoryPath;
        switch (args.Length)
        {
            case 0:
                Random random = new Random();
                UpdateCreationTime(Directory.GetCurrentDirectory(), Convert.ToInt32(random.Next(0, 700000001)));
                break;

            case 1:
                directoryPath = Directory.GetCurrentDirectory();
                UpdateCreationTime(directoryPath, Convert.ToInt32(args[1]));
                break;

            case 2:
                Directory.SetCurrentDirectory(args[0]);
                directoryPath = Directory.GetCurrentDirectory();
                UpdateCreationTime(directoryPath, Convert.ToInt32(args[1]));
                break;

            default:
                break;
        }
    }

    public static DateTime UnixTimeStampToDateTime(double creationTime)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(creationTime).ToLocalTime();
        return dateTime;
    }

    private static void UpdateCreationTime(string directoryPath, Int32 arg)
    {
        try
        {
            foreach (string d in Directory.GetDirectories(directoryPath))
            {

                DirectoryInfo directoryInfo = new DirectoryInfo(d);
                directoryInfo.CreationTime = UnixTimeStampToDateTime(777777777 + arg);
                directoryInfo.LastWriteTime = UnixTimeStampToDateTime(888888888 + arg);
                directoryInfo.LastAccessTime = UnixTimeStampToDateTime(999999999 + arg);
                Console.WriteLine("Updated Directory Info for Directory: " + directoryInfo.FullName);

                UpdateCreationTime(d, arg);

            }
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                double timeBase = Convert.ToDouble(file.GetHashCode());

                FileInfo fileInfo = new FileInfo(file);
                fileInfo.IsReadOnly = false;
                fileInfo.CreationTime = UnixTimeStampToDateTime(999999999 + arg);
                fileInfo.LastWriteTime = UnixTimeStampToDateTime(999999999 + arg);
                fileInfo.LastAccessTime = UnixTimeStampToDateTime(999999999 + arg);
                Console.WriteLine("Updated time for file: " + fileInfo.FullName);
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}


