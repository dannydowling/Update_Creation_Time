using System.Globalization;

public class UpdateFileCreationTime
{
  

    public static void Main(string[] args)
    {
        string directoryPath = Directory.GetCurrentDirectory();
        double creationTime = 629557261;

        if (args.Length != 0)
        { creationTime = Convert.ToDouble(args[0]);  }       
        var timeStamp = UnixTimeStampToDateTime(creationTime);

        UpdateCreationTime(directoryPath, timeStamp);
        
    }
  
    public static DateTime UnixTimeStampToDateTime(double creationTime)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(creationTime).ToLocalTime();
        return dateTime;
    }

    private static void UpdateCreationTime(string directoryPath, DateTime timeStamp)
    {
        try
        {
            foreach (string d in Directory.GetDirectories(directoryPath))
            {
                UpdateCreationTime(d, UnixTimeStampToDateTime(629557261));
                DirectoryInfo directoryInfo = new DirectoryInfo(d);
                directoryInfo.CreationTime = UnixTimeStampToDateTime(629557261);
                directoryInfo.LastWriteTime = UnixTimeStampToDateTime(629557262);
                directoryInfo.LastAccessTime = UnixTimeStampToDateTime(629557263);
                Console.WriteLine("Updated Directory Info for Directory: " + directoryInfo.FullName);

            }
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                double timeBase = Convert.ToDouble(file.GetHashCode());
                var randomBase = new Random();

                FileInfo fileInfo = new FileInfo(file);
                fileInfo.IsReadOnly = false;
                fileInfo.CreationTime = UnixTimeStampToDateTime(62956269);
                fileInfo.LastWriteTime = UnixTimeStampToDateTime(629558270);
                fileInfo.LastAccessTime = UnixTimeStampToDateTime(629558271);               
                Console.WriteLine("Updated time for file: " + fileInfo.FullName);
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine("eeroooorrrr");
        }
    
    }
}


