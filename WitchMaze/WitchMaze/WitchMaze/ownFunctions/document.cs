using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WitchMaze.ownFunctions
{
    public class Document
    {
        string path;

        /// <summary>
        /// if there is no file, it makes a new one, else nothing happens
        /// Am besten für jedes was man speichern will, eine neue txt. Datei, dann brauch nur ein Wort gespeichert werden, das ist einfacher zum ein und auslesen 
        /// </summary>
        /// <param name="_docuemntname">name of the document</param>
        /// <param name="_path">path of document (for exmaple) "Content/settings.txt"</param>
        public Document(string _path)
        {
            path = _path;
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path);
            }
            
        }


        /// <summary>
        /// method to write in the file, deletes everything that was in there before
        /// </summary>
        /// <param name="whatToWrite">the text you want to write into the file</param>
        public void write(String whatToWrite)
        {
            System.IO.File.WriteAllText(path, whatToWrite);
        }


        /// <summary>
        /// methode to read a file, returns a string with the whole content of the file, or "no file to read" if there i no file
        /// </summary>
        /// <returns>string with content</returns>
        public string read()
        {
            if (System.IO.File.Exists(path))
                return System.IO.File.ReadAllText(path);
            else
                return "no file to read";
        }


        

        
    }
}
