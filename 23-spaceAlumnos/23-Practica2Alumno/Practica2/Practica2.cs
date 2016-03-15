using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.poo.invaders;
using System.Windows.Forms;

namespace Practica2
{
    public class Practica2
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PantallaJuego juego = new PantallaJuego();
            Application.Run(juego);
            
        }
    }
}
