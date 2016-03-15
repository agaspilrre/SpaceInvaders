using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.poo.invaders
{
    /**
     * Esta clase representa la nave del jugador
     * 
     */
    public class Nave:MOB
    {
        /**
	     * Crea una nueva entidad que representa la nave del jugador
	     * 
	     * @param rutaImagen
	     *            Ruta al fichero que tiene la representacion de la nave del
	     *            jugador
	     * @param x
	     *            Posicion x inicial de la nave del jugador
	     * @param y
	     *            Posicion y inicial de la nave del jugador
	     * @throws CargaSpriteException
	     *             se lanza si la ruta de la imagen que se pasa como parametro
	     *             es nula o bien hay algún problema al cargar el fichero que
	     *             contiene la imagen que representa el sprite
	     * 
	     */
	    public Nave(string rutaImagen, int x, int y):base(rutaImagen,x,y) {
            // TO-DO
            //PARAMETROS DE ESTE METODO SE LOS PASO AL CONTRUCTOR DE MOB
            //ECEPCION YA LA HACE DESDE LA CLASE SPRITEFACTORY

	    }

        /**
         * Modifica la posicion del MB en funcion de las velocidades de movimiento y
         * del tiempo. 
         * Si la nave se mueve a la izquierda y llega al borde de la
         * pantalla no se mueve la nave se mueve a la derecha y llega al borde de la
         * pantalla no se mueve. En cualquier otro caso nos movemos
         * 
         * @param tiempo
         *            Cantidad de tiempo expresada en milisegundos
         */
        public override void mover(long tiempo) {
            // TO-DO
            //SI LA X ESTA ENTRE ESTAS POSICIONES PODEMOS MOVER LLAMAMOS AL METODO MOVER DEL PADRE
            if (obtenerPosicionX() >= 0 && obtenerPosicionX() <= 720)
            {
                base.mover(tiempo);

            }

            //SI EL OBJETO NAVE ESTA FUERA DEl MARGEN IZQ Y SU VELOCIDAD HORIZONTAL ES POSITIVA O ESTA EN EL MARGEN DERECHO Y LA VELOCIDAD HORIZONTAL ES NEGATIVA ENTONCES PUEDO MOVER LA NAVE
            if((obtenerPosicionX() <= 20 && this.obtenerVelocidadHorizontal() > 0) || (obtenerPosicionX() >= 720 && obtenerVelocidadHorizontal()<0))
            {
                
                base.mover(tiempo);
                

            }



        }

	    /**
	     * Gestiona lo que tiene que ocurrir cuando un MOB choca con otro MOB
	     * Si la nave choca con un enemigo, el jugador pierde. Hay que hacer uso
         * de establecerHaPerdido de la clase controlador
         * 
	     * @param colisionado
	     *            MOB con el que se colisiona
	     */
        public  override void colisionarCon(MOB colisionado)
        {
            // TO-DO
            bool comprobador =base.chocarCon(colisionado);//VARIABLE TIPO BOOL QUE GUARDA TRUE O FALSE DEPENDIENDO DE LO QUE RETORNE LA FUNCION A LA QUE LLAMA
            Alien alienAux;//VARIABLE TIPO ALIEN
            alienAux=colisionado as Alien;//SI EL PARAMETRO QUE RECIBIMOS ES UN OBJETO ALIEN LO GUARDA EN LA VARIABLE SI NO GUARDA UN NULL

            if (comprobador == true && alienAux!=null)
            {
                this.obtenerControladorJuego().establecerHaPerdido(true);//SI ESTE OBJETO CHOCA CON OTRO Y ES UN OBJETO DE TIPO ALIEN LLAMA A LA FUNCION ESTABLECER HA PERDIDO
            }

	    }
    }
}
