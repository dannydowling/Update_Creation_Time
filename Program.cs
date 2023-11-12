using System.Globalization;

public class UpdateFileCreationTime
{
  

    public static void Main(string[] args)
    {
        string directoryPath = Directory.GetCurrentDirectory();
        double creationTime = 629514061;

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
                UpdateCreationTime(d, timeStamp);

            }
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                double timeBase = Convert.ToDouble(file.GetHashCode());
                var randomBase = new Random();

                FileInfo fileInfo = new FileInfo(file);
                fileInfo.CreationTime = timeStamp;             
                fileInfo.LastWriteTime = UnixTimeStampToDateTime(randomBase.Next(1, 1337));
                fileInfo.LastAccessTime = UnixTimeStampToDateTime(randomBase.Next(1337, 2660));
                Console.WriteLine("Updated time for file: " + fileInfo.FullName);
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine("eeroooorrrr");
        }
    
    }
}


