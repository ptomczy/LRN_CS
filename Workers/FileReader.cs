using System;
using System.IO;

namespace LRN_CS.Worker
{
    class FileReader
    {
        internal string[] Read(string fileName)
        {
            string[] fileContent;
            try
            {
                fileContent = File.ReadAllLines(fileName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

            return fileContent;
        }
    }
}
