using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace org.poo.invaders
{
    /**
     * 
     * Controlador del juego. Representa la logica del juego. Gestiona la lista de
     * MOB que hay actualmente en el juego
     * 
     */
    public partial class ControladorJuego
    {
        /**
	     * Constante que representa la velocidad inicial de movimiento del
	     * alienigena (pixels/sec)
	     */
	    public static readonly int VELOCIDAD_INICIAL_ALIEN = 55;
	    /**
	     * Constante que representa la velocidad inicial de movimiento del proyectil
	     * (pixels/sec)
	     */
	    public static readonly int VELOCIDAD_INICIAL_PROYECTIL = -300;
	    /**
	     * Constante que representa la velocidad inicial de movimiento de la nave
	     * (pixels/sec)
	     */
	    public static double VELOCIDAD_NAVE = -300;
	    /** Intervalo entre disparos del jugador (ms) */
	    public static readonly long INTERVALO_DISPARO = 500;

        /** Numero de aliens en el juego */
        private int numeroAliens;
        /** Nave del jugador. Tambien esta el la lista de mobs */
        private MOB nave;
        /** Momento del ultimo disparo */
        private long ultimoDisparo = 0;

        /**
         * true si hay que desplazar en vertical a los aliens
         */
        private bool desplazamientoAlienVerticalNecesario = false;

        /**
         * Indicador de si el jugador ha eliminado todos los aliens
         */
        private bool gana = false;
        /**
         * Indicador de si el jugador ha chocado con un alien o algun alien ha
         * llegado a la parte inferior de la pantalla
         */
        private bool pierde = false;
	    
	    /**
	     * Constructor del controlador. Inicializa el controlador del juego
	     * 
	     * @throws CargaSpriteException
	     *             se lanza si algun sprite de los MOB que participan en el
	     *             juego no se puede cargar correctamente
	     */
	    public ControladorJuego() {
		    // incializa los MOB del juego
		    inicializar();
	    }

	    /**
	     * Intenta disparar un proyectil del jugador. No se puede dispara
	     * continuamente. Antes de dispara de nuevo hay que esperar un tiempo
	     * 
	     * @throws VelocidadErroneaException
	     *             si la velocidad del proyectil es erronea. Un proyectil solo
	     *             se mueve hacia arriba (valor de velocidad negativo)
	     * @throws CargaSpriteException
	     *             se lanza si el sprite del proyectil no se puede cargar
	     *             correctamente
	     * 
	     */
	    public void disparar() {
		    // comprobar que podemos dispara, ha pasado el tiempo suficiente. Si ha
		    // pasado el tiempo suficiente, generar un nuevo proyectil y registrar
		    // el momento del disparo
            if ((Environment.TickCount & Int32.MaxValue) - ultimoDisparo >= INTERVALO_DISPARO)
            {
			    // if we waited long enough, create the shot entity, and record the
			    // time.
                ultimoDisparo = Environment.TickCount & Int32.MaxValue;
			    Proyectil disparo = new Proyectil("disparo",
					    nave.obtenerPosicionX() + 10, nave.obtenerPosicionY() - 30,
					    ControladorJuego.VELOCIDAD_INICIAL_PROYECTIL);
			    disparo.establecerControladorJuego(this);
			    mobs.Add(disparo);
		    }

	    }


	    /**
	     * Elimina de la lista de mobs aquellos que han sido eliminados y borrar
	     * todo el contenido de la lista de mobs eliminados
	     */
	    private void actualizarMOBs() {
            foreach(MOB mobEliminado in mobsEliminados){
                mobs.Remove(mobEliminado);
            }
            mobsEliminados.Clear();
	    }

	    /**
	     * Restablece todos los elementos del contorlador del juego. Elimina los
	     * mobs
	     */
	    private void reset() {
		    mobs.Clear();
		    numeroAliens = 0;
		    mobsEliminados.Clear();
		    nave = null;
		    // pierde = false;
		    // gana = false;
	    }

	    /**
	     * Devuelve una referenecia a la nave que participa en el controlador del
	     * juego
	     * 
	     * @return la nave que participa en el juego
	     */
	    public MOB getShip() {
		    return nave;
	    }

	    /**
	     * Indica si el jugador gana la partida
	     * 
	     * @return true si el jugador ha ganado al partida, false en otro caso
	     */
	    public bool haGanado() {
		    return gana;
	    }

	    /**
	     * Modifica el vlor de la propiedad gana
	     * 
	     * @param gana
	     *            true si el jugador gana la partida, false en otro caso
	     */
	    public void establecerHaGanado(bool gana) {
		    this.gana = gana;
	    }

	    /**
	     * Indica si el jugador ha perdido la partida
	     * 
	     * @return true si el jugador ha perdido la partida, false en otro caso
	     */
	    public bool haPerdido() {
		    return pierde;
	    }

	    /**
	     * Modifica el vlor de la propiedad gana
	     * 
	     * @param gana
	     *            true si el jugador gana la partida, false en otro caso
	     */
	    public void establecerHaPerdido(bool pierde) {
		    this.pierde = pierde;
	    }

	    /**
	     * Establece el valor de la propiedad desplazamientoAlienVerticalNecesario
	     * 
	     * @param desplazamientoAlienVerticalNecesario
	     *            true si es necesario el desplazamiento vertical de los aliens
	     */
	    public void establecerDesplazamientoAlienVerticalNecesario(
			    bool desplazamientoAlienVerticalNecesario) {
		    this.desplazamientoAlienVerticalNecesario = desplazamientoAlienVerticalNecesario;
	    }
    }
}
