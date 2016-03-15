
using System;
using System.Collections.Generic;
using org.poo.invaders.exceptions;
using System.Linq;
using System.Text;

namespace org.poo.invaders
{
    /**
     * Representa un proyectil disparado por la nave del jugador
     * 
     */
    public class Proyectil :MOB
    {
        /**
	     * Indica si el proyectil ha sido ya utilizado, o lo que es lo mismo, ha
	     * colisionado con algo
	     */
	    private bool usado = false;

	    /**
	     * Crea un nuevo proyectil. Un proyectil sólo puede tener movimiento
	     * vertical
	     * 
	     * @param rutaImagen
	     *            Ruta al fichero que tiene la representación del proyectil
	     * @param x
	     *            Posicion x inicial del proyectil
	     * @param y
	     *            Posicion y inicial del proyectil
	     * @param velocidadVertical
	     *            Velocidad inicial del proyectil (pixeles/segundo)
	     * @throws CargaSpriteException
	     *             se lanza si la ruta de la imagen que se pasa como parametro
	     *             es nula o bien hay algún problema al cargar el fichero que
	     *             contiene la imagen que representa el sprite
	     * @throws VelocidadErroneaException
	     *             se lanza si velocidadVertical es mayor o igual que cero. Un
	     *             proyectil solo puede tener velocidad negativa
	     */
	    public Proyectil(string rutaImagen, int x, int y, double velocidadVertical):base(rutaImagen,x,y){
            // TO-DO

            //SI LA VELOCIDAD ES CERO LANZO LA EXCEPCION SI NO EJECUTO EL CODIGO NORMALMENTE
            if (velocidadVertical >= 0)
            {
                throw new VelocidadErroneaException("un proyectil solo puede tener velocidad negativa");
            }

            else
            {
                establecerVelocidadVertical(velocidadVertical);//ESTABLEZCO AL OBJETO PROYECTIL LA VELOCIDAD QUE RECIBE COMO PARAMETRO
                                                               //EL RESTO DE PARAMETROS SE LOS PASA AL PADRE PARA QUE LOS GESTIONE

            }


        }

        /**
         * Modifica la posicion del MB en funcion de las velocidades de movimiento y
         * del tiempo
         * 
         * si el proyectil sale de la pantalla hay que eliminarlo
         * 
         * @param tiempo
         *            Cantidad de tiempo expresada en milisegundos
         */
        public override void mover(long tiempo) {
            // TO-DO

            base.mover(tiempo);//ASIGNO MOVIMIENTO AL PROYECTIL

            //SI EN LA COORDENADA DE Y SE PASA DE CERO QUE ES EL ALTO DE LA PANTALLA ELIMINO EL PROYECTIL
            if (obtenerPosicionY() < 0)
            {
                this.obtenerControladorJuego().eliminarMOB(this);
            }


	    }

        /**
         * Gestiona lo que teien que ocurrir cuando un MOB choca con otro MOB
         * 
         * Si la colision es con un alienigena, el alienigena debe
         * morir y el proyectil desaparecer. Cuando un proyectil alcanza un alien
         * hay que hacer uso del metodo notificarAlienAbatido de la clase Controlador
         * 
         * @param colisionado
         *            MOB con el que se colisiona
         */
        public override void colisionarCon(MOB colisionado) {

            bool comprobador = base.chocarCon(colisionado);//VARIABLE BOOL QUE GUARDA LO QUE RETORNA LA FUNCION A LA QUE LLAMA
            Alien alienAux = colisionado as Alien;//SI EL OBJETO QUE RECIBE COMO PARAMETRO ES UN ALIEN LO GURADA SI NO GUARDA UN NULL

            //SI COMPROBADOR ES VERDADERP Y ALIENAUX ES DISTINTO DE NULL
            if (comprobador == true && alienAux != null)
            {
                this.obtenerControladorJuego().eliminarMOB(this);//ELIMINO EL PROYECTIL
                this.obtenerControladorJuego().eliminarMOB(colisionado);//ELIMINO EL ALIEN CON EL QUE HA COLISIONADO
                this.obtenerControladorJuego().notificarAlienAbatido();//LLAMO A LA FUNCION ALIEN ABATIDO PARA QUE EJECUTE SU FUNCIONALIDAD
                
            }

        }
    }
}
