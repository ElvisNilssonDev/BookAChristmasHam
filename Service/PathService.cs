namespace BookAChristmasHam.Service
{
    //– visar filvägar och skapar data-mappen om den inte finns
    public static class PathService
    {
        public static string GetDataFilePath(string filePath)
        {
            string projectRoot = Path.Combine(AppContext.BaseDirectory, "..", "..", "..");
            string dataFolder = Path.Combine(projectRoot, "Data");
            Directory.CreateDirectory(dataFolder);
            return Path.Combine(dataFolder, filePath);
        }
        public static string GetRelativePath(string filePath)
        {
            var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
            return Path.GetRelativePath(projectRoot, Path.GetFullPath(filePath));
        }
    }
}