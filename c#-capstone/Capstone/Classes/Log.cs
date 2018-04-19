using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class Log
    {
        public static void UpdateLog(string activity)
        {
            DateTime timeStamp = DateTime.Now;

            string logString = $"{timeStamp.ToShortDateString()} {timeStamp.ToShortTimeString()} {activity}";

            string destinationFolder = @"..\..\..\etc";
            bool directroyExists = Directory.Exists(destinationFolder);

            if (directroyExists)
            {
                try
                {
                    using (FileStream fS = new FileStream(Path.Combine(destinationFolder, "log.txt"), FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter sW = new StreamWriter(fS))
                        {
                            sW.WriteLine(logString);
                        }
                    }
                }
                catch (Exception)
                {

                    throw new Exception("Log error");
                }
                
            }
        }
    }
}
