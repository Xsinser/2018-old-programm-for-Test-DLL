using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication10
{
static class DataProfil
    {
        static string NameProfil;
        static string ProfilPth;
    static    public string Name
        {
            get
            {
                return NameProfil;
            }

            set
            {
                NameProfil = value;
            }
        }

        static public string PathForProfil
        {
            get
            {
                return ProfilPth;
            }

            set
            {
                ProfilPth = value;
            }
        }
    }
}
