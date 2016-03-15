using System;
using System.Collections.Generic;
using org.poo.invaders.exceptions;//using para comunicar namespaces para poder comunicar las excepciones
using System.Linq;
using System.Text;


namespace org.poo.invaders
{
    /**
     * Esta clase representa el alien invasor
     * 
     */
    public class Alien:MOB
    {
        /**
	     * Crea un nuevo alienigena invasor
	     * 
	     * @param rutaImagen
	     *            Ruta al fichero que tiene la representación del alienigena
	     *            invasor
	     * @param x
	     *            Posicion x inicial del alien
	     * @param y
	     *            Posicion y inicial del alien
	     * @throws CargaSpriteException
	     *             se lanza si la ruta de la imagen que se pasa como parametro
	     *             es nula o bien hay algún problema al cargar el fichero que
	     *             contiene la imagen que representa el sprite
	     * 
	     */
	    public Alien(string rutaImagen, int x, int y, double velocidadHorizontal):base (rutaImagen,x,y)
	    {
            // TO-DO

            //BASE HEREDA DE LA CLASE MOB Y LE PASO LOS PARAMETROS AL CONSTRUCTOR
            establecerVelocidadHorizontal(velocidadHorizontal);//INVOCO AL METODO DE MOB Y LE PASO COMO PARAMETRO LA VELOCIDAD HORIZONTAL RECIBIDA



        }

	    /**
	     * Desplaza el alienigena en vertical y cambia la direccion de su
	     * desplazamiento horizontal.
	     * 
	     * @param velocidadVertical
	     *            Numero de pixeles/segundo que baja el alien (valor positivo)
	     * @throws VelocidadErroneaException
	     *             se lanza si cantidad es menor que cero. Un alien solo se
	     *             mueve hacia abajo en la pantalla en su desplazamiento
	     *             vertical
	     */
	    public void bajar(int velocidadVertical) {
            // TO-DO

            if (velocidadVertical < 0)
            {
                throw new VelocidadErroneaException("Un alien solo se mueve hacia abajo en la pantalla en su desplazamiento");
            }

            else
            {
                establecerVelocidadVertical(velocidadVertical);//RECIBO UN PARAMETRO INT QUE ASIGNO LLAMANDO A LA FUNCION ESTABLECER VELOCIDADVERTICAL




                establecerVelocidadHorizontal(-obtenerVelocidadHorizontal());//CAMBIO EL SENTIDO DE LOS ALIEN CAMBIANDO LA VELOCIDADHORIZONTAL A VALOR NEGATIVO

            }
            
            
           
            
        }

        /**
         * Modifica la posicion del MB en funcion de las velocidades de movimiento y
         * del tiempo
         * 
         *  Si el alienigena se mueve a la izquierda y llega al borde de la pantalla cambiar 
         *  el sentido de desplazamiento y avisar para cambiar el sentido de desplazamiento de 
         *  todos los  alienigenas y bajar una línea todos los alienigenas
         *  O
         *  Si el alienigena se mueve a la derecha y llega al borde de la
         *  pantalla cambiar el sentido de desplazamiento y avisar para
         *  cambiar el sentido de desplazamiento de todos los alienigenas y bajar una línea todos los alienigenas
         * 
         * @param tiempo
         *            Cantidad de tiempo expresada en milisegundos
         */
        public override void mover(long tiempo) {
            // TO-DO

            base.mover(tiempo);//LLAMO A LA FUNCION MOVER DEL PADRE Y LE PASO EL PARAMETRO DE TIEMPO RECIBIDO

            //SI LA POSICION EN X DE LOS ALIENS LLEGA A LOS LIMITES DE LA PANTALLA
            if (this.obtenerPosicionX() <= 4 || this.obtenerPosicionX() >= 725)
            {
                this.obtenerControladorJuego().establecerDesplazamientoAlienVerticalNecesario(true);//LLAMO A LA FUNCION DE CONTROLADOR PARA PONER A TRUE EL DESPLAZAMIENTO VERTICAL NECESARIO DEL ALIEN
            }
            
	    }

	    /**
	     * Gestiona lo que tiene que ocurrir cuando un MOB choca con otro MOB
	     * 
	     * @param colisionado
	     *            MOB con el que se colisiona
	     */
        public override void colisionarCon(MOB colisionado)
        {
		    // Un alien si choca con algo delega en el objeto con el que choca
		    colisionado.colisionarCon(this);
	    }
    }
}