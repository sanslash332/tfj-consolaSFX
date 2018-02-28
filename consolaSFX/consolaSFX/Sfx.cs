using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace consolaSFX
{
    public class Sfx
    {
        public String sfxName;
        public string sfxPath;
        public Keys sfxKey;
        public String listString;
        public bool inclControl;
        public bool inclAlt;
        public bool inclShift;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sfxp"></param>
        /// <param name="key"></param>
        /// <param name="ctrl"></param>
        /// <param name="shift"></param>
        /// <param name="alt"></param>
        public Sfx(string sfxp, Keys key, bool ctrl, bool shift, bool alt)
        {
            this.sfxPath = sfxp;
            this.sfxKey = key;
            this.sfxName = Path.GetFileName(sfxPath);
            this.inclAlt = alt;
            this.inclControl = ctrl;
            this.inclShift = shift;
            this.generateListString();

            
        }

        private void generateListString()
        {
            this.listString = this.sfxName + ", convinación de teclas: ";
            if(this.inclControl)
            {
                this.listString += "ctrl+";
            }
            if(this.inclAlt)
            {
                this.listString += "alt+";

            }
            if(this.inclShift)
            {
                this.listString += "shift+";
            }
            if(this.sfxKey!=null)
            {
                this.listString += this.sfxKey.ToString();

            }




        }


    }
}
