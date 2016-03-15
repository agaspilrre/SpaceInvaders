using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using org.poo.invaders.exceptions;
using System.Threading;

namespace org.poo.invaders
{
    public partial class PantallaJuego : Form
    {
        public static readonly int ANCHO_PANTALLA = 800;
        public static readonly int ALTO_PANTALLA = 600;

        /** true si el juego esta en ejecucion */
        private bool jugando = true;

        /** mensaje a mostrar en pantalla */
        private String mensaje = "";
        /** True si se esta esperando la pulsacion de una teclas */
        private bool esperandoTeclaInicioPartida = true;
        /** True si se pulsa el cursor izquierdo para mover la nave */
        private bool moverIzquierda = false;
        /** True si se pulsa el cursor derecha para mover la nave */
        private bool moverDerecha = false;
        /** True si disparamos (presionar espacio) */
        private bool espacioPresionado = false;

        private ControladorJuego controlador;

        private Point Position = new Point(0, 0);
        private Font MyFont = new Font("Compact", 20.0f, GraphicsUnit.Pixel);

        private long momentoUltimoMovimiento;

        /**
	    * Constructor de la pantalla del juego. Crear la pantalla
	    */
        public PantallaJuego()
        {
            InitializeComponent();

             //crear el controlador del juego
            try
            {
                this.controlador = new ControladorJuego();
            }
            catch (CargaSpriteException e1)
            {
                MessageBox.Show(e1.Message);
                Application.Exit();
            }
            momentoUltimoMovimiento = Environment.TickCount & Int32.MaxValue;
            timer1.Start();
        }

        /**
	    * Inicializa un nuevo juego, reiniciando todos los elementos
	    */
        private void startGame()
        {
            try
            {
                controlador.inicializar();
                moverIzquierda = false;
                moverDerecha = false;
                espacioPresionado = false;
            }
            catch (CargaSpriteException e)
            {
                Application.Exit();
            }
        }

        /**
	     * Notifica que el jugador ha perdido la partida
	     */
        public void perderPartida()
        {
            mensaje = "no! te han capturado los alienigenas, ¿intentar otra vez?";
            esperandoTeclaInicioPartida = true;
        }

        /**
         * Notifica que el jugador ha ganado, ha eliminado todos los alienigena
         */
        public void ganarPartida()
        {
            mensaje = "Bien hecho! Has ganado!, ¿jugar otra vez?";
            esperandoTeclaInicioPartida = true;
        }

        /**
	     * Pone el juego en ejecucion
	    */
        public void jugar() {
		    

		    // mantiene el juego en ejecucion hasta que se presiona ESC
            
		    if (jugando) {
			    // tiempo transcurrido desde el ultimo movimiento
			    long delta = (Environment.TickCount & Int32.MaxValue) - momentoUltimoMovimiento;

			    momentoUltimoMovimiento = Environment.TickCount & Int32.MaxValue;

                // Si no hay una tecla pulsada se mueven los mobs
                if (!esperandoTeclaInicioPartida)
                {
                    try
                    {
                        this.controlador.mover(delta);
                    }
                    catch (VelocidadErroneaException e)
                    {
                        MessageBox.Show(e.Message);
                        Application.Exit();
                    }
                }
                // Terminar la partida en caso de ganar o perder
                if (this.controlador.haGanado())
                    this.ganarPartida();
                else if (this.controlador.haPerdido())
                    this.perderPartida();

                //// la velocidad de movimiento de la nave es 0, mientras no se
                //// presiona el cursor izquierdo o derecho
                controlador.getShip().establecerVelocidadHorizontal(0);

                //// gestiona el movimiento de la nave a la derecha o a la izquierda
                if ((moverIzquierda) && (!moverDerecha))
                {
                    controlador.getShip().establecerVelocidadHorizontal(
                            ControladorJuego.VELOCIDAD_NAVE);
                }
                else if ((moverDerecha) && (!moverIzquierda))
                {
                    controlador.getShip().establecerVelocidadHorizontal(
                            -ControladorJuego.VELOCIDAD_NAVE);
                }

                //// si se presiona la tecla de espacio se dispara si es posible
                if (espacioPresionado) {
                    try
                    {
                        controlador.disparar();
                    }
                    catch (CargaSpriteException e)
                    {
                        MessageBox.Show(e.Message);
                        Application.Exit();
                    }
                    catch (VelocidadErroneaException e)
                    {
                        MessageBox.Show(e.Message);
                        Application.Exit();
                    }
                }
                this.Invalidate();

                //// pausamos 10 ms para conseguir un refresco de 100fps
                //try
                //{
                //    Thread.Sleep(10);
                //}
                //catch (ArgumentOutOfRangeException e)
                //{
                //    MessageBox.Show(e.Message);
                //}
            }
            //this.Refresh();

            ////// pausamos 10 ms para conseguir un refresco de 100fps
            //try {
            //    Thread.Sleep(10);
            //} catch (ArgumentOutOfRangeException e) {
            //    MessageBox.Show(e.Message);
            //}
        }

        private void PantallaJuego_KeyDown(object sender, KeyEventArgs e)
        {
            string tecla = e.KeyData.ToString();

            // si estamos jugando (esperandoTeclaPartida = false) gestionar los
            // cursores y el espacio
            if (!esperandoTeclaInicioPartida
                    && tecla == "Left")
            {
                moverIzquierda = true;
            }
            if (!esperandoTeclaInicioPartida
                    && tecla == "Right")
            {
                moverDerecha = true;
            }
            if (!esperandoTeclaInicioPartida
                    && tecla == "Space")
            {
                espacioPresionado = true;
            }
        }

        private void PantallaJuego_KeyUp(object sender, KeyEventArgs e)
        {
            string tecla = e.KeyData.ToString();
            if (!esperandoTeclaInicioPartida
					&& tecla == "Left") {
				moverIzquierda = false;
			}
			if (!esperandoTeclaInicioPartida
					&& tecla == "Right") {
				moverDerecha = false;
			}
			if (!esperandoTeclaInicioPartida
					&& tecla == "Space") {
				espacioPresionado = false;
			}
		}

        private void PantallaJuego_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (esperandoTeclaInicioPartida) {
				esperandoTeclaInicioPartida = false;
			    startGame();
			}
            if (e.KeyChar == 27)
            {
                Application.Exit();
            }
        }

        private void PantallaJuego_Paint(object sender, PaintEventArgs e)
        {
            Graphics pantalla = e.Graphics;

            // Pinta de color negro el area de juego
            pantalla.FillRectangle(Brushes.Black, 0, 0, ClientRectangle.Width, ClientRectangle.Height);

            // dibujar los mobs en la pantalla del juego
            this.controlador.representar(pantalla);
            // Si estoy esperando para comenzar la partida, escribir, presiona
            // una tecla
            if (esperandoTeclaInicioPartida)
            {
                pantalla.DrawString(mensaje, MyFont, Brushes.White, new Point(((int)(800 - (MyFont.SizeInPoints * mensaje.Length
                        )) / 2), 250));
                pantalla.DrawString(
                        "Presiona una tecla", MyFont, Brushes.White,
                        new Point(((int)(800 - (MyFont.SizeInPoints * "Presiona una tecla".Length
                        )) / 2), 300));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            jugar();
        }
    }
}
