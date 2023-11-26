using System.Globalization;

public class UpdateFileCreationTime
{
    public static void Main(Int32 arg)
    {
        string directoryPath = Directory.GetCurrentDirectory();
     if (arg == 0 ) { Random rnd = new Random();
            arg = rnd.Next(1, 700000001);
        }
        UpdateCreationTime(directoryPath, arg);        
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


