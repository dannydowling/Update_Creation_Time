using System.Globalization;

public class UpdateFileCreationTime
{
    public static void Main(string[] args)
    {
        string directoryPath = Directory.GetCurrentDirectory();
        double creationTime;

        if (args.Length != 0)
        {  creationTime = Convert.ToDouble(args[0]);  }
        else
        {  creationTime = Convert.ToDouble(DateTime.Now);  }

        UpdateCreationTime(directoryPath, creationTime);
        
    }
  
    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dateTime;
    }

    private static void UpdateCreationTime(string directoryPath, double creationTime)
    {
        try
        {
            foreach (string d in Directory.GetDirectories(directoryPath))
            {
                UpdateCreationTime(d, creationTime);

            }
            foreach (var file in Directory.GetFiles(directoryPath))
            {
                double timeBase = Convert.ToDouble(file.GetHashCode());
                var randomBase = new Random();

                FileInfo fileInfo = new FileInfo(file);
                fileInfo.CreationTime = UnixTimeStampToDateTime(creationTime);             
                fileInfo.LastWriteTime = UnixTimeStampToDateTime(creationTime = randomBase.Next(1, 166365));
                fileInfo.LastAccessTime = UnixTimeStampToDateTime(creationTime + randomBase.Next(166365, 322685));
                Console.WriteLine("Updated time for file: " + fileInfo.FullName);
            }
        }
        catch (System.Exception e)
        {
            Console.WriteLine("eeroooorrrr");
        }
    
    }
}


