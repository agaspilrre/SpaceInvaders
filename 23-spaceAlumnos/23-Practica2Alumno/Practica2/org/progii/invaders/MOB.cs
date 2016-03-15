using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace org.poo.invaders
{
    /**
     * 
     * Un MOB representa cualquier elemento que aparece en el juego. Es responsable
     * de resolver las colisiones y el movimiento
     * 
     */
    public abstract class MOB
    {
        /** Posición actual del MOB en el eje x */
	    private int x;
	    /** Posición actual del MOB en el eje y */
	    private int y;
	    /** Imagen utilizada para representar el MOB en la pantalla */
	    private Sprite sprite;
	    /**
	     * Velocidad de desplazamiento en el eje X (pixeles/segundo). Valor
	     * positivo, se mueve a la derecha, valor negativo, se mueve a la izquierda
	     */
	    private double velocidadHorizontal;
	    /**
	     * Velocidad de desplazamiento en el eje Y (pixeles/segundo). Valor
	     * positivo, se mueve hacia a abajo, valor negativo, se mueve hacia a arriba
	     */
	    private double velocidadVertical;

	    /**
	     * Controlador de juego donde participa el MOB
	     * 
	     */
	    private ControladorJuego controladorJuego;

	    /**
	     * Construye un MOB con un sprite y una localización dados
	     * 
	     * @param imagen
	     *            Ruta de la imagen que representa el sprite asociado al MOB
	     * @param x
	     *            Posición horizontal inicial del MOB
	     * @param y
	     *            Localización vertical inicial del MOB
	     * @throws CargaSpriteException
	     *             se lanza si la ruta de la imagen que se pasa como parametro
	     *             es nula o bien hay algún problema al cargar el fichero que
	     *             contiene la imagen que representa el sprite
	     * 
	     */
	    public MOB(string imagen, int x, int y) {
            // TO-DO

            
                SpriteFactory rutaSprite = new SpriteFactory();//creo objeto spriteFactory
                sprite = rutaSprite.obtenerSprite(imagen); //asigno al atributo sprite el sprite que obtengo del metodo obtenerSprite pasandole el parametro string imagen que contiene la ruta del sprite.
                                                           
           
                //asigno al atributo x e y los parametros x e y.
                this.x = x;
                this.y = y; 



	    }

	    /**
	     * Modifica la posicion del MB en funcion de las velocidades de movimiento y
	     * del tiempo
	     * 
	     * @param tiempo
	     *            Cantidad de tiempo expresada en milisegundos
	     */
	    public virtual void mover(long tiempo) {
		    // modifica la posicion del MOB basandose en las velocidades de
		    // movimiento
		    x += (int)(tiempo * velocidadHorizontal) / 1000;
		    y += (int)(tiempo * velocidadVertical) / 1000;
	    }

	    /**
	     * Establece la velocidad de desplazamiento horizontal del MOB
	     * 
	     * @param velocidadHorizontal
	     *            Velocidad horizontal del MOB (pixeles/segundo). Valor
	     *            positivo, se mueve a la derecha, valor negativo, se mueve a la
	     *            izquierda
	     */
	    public void establecerVelocidadHorizontal(double velocidadHorizontal) {
            // TO-DO

            this.velocidadHorizontal = velocidadHorizontal;//asigno al atributo de esta clase el parametro recibido
	    }

	    /**
	     * Establece la velocidad de desplazamiento vertical del MOB
	     * 
	     * @param velocidadVertical
	     *            Velocidad vertical del MOB (pixeles/segundo). Valor positivo,
	     *            se mueve hacia a abajo, valor negativo, se mueve hacia a
	     *            arriba
	     */
	    public void establecerVelocidadVertical(double velocidadVertical) {
            // TO-DO

            this.velocidadVertical = velocidadVertical;//ASIGNO AL ATRIBUTO EL PARAMETRO RECIBIDO
	    }

	    /**
	     * Devuelve la velocidad horizontal del MOB
	     * 
	     * @return Velocidad horizontal del MOB en pixeles/segundo. Valor positivo,
	     *         se mueve a la derecha, valor negativo, se mueve a la izquierda
	     */
	    public double obtenerVelocidadHorizontal() {
            // TO-DO

            return this.velocidadHorizontal;//DEVUELVE EL VALOR DE VELOCIDAD HORIZONTAL
	    }

	    /**
	     * Devuelve la velocidad vertical del MOB
	     * 
	     * @return Velocidad vertical del MOB en pixeles/segundo. Valor positivo, se
	     *         mueve hacia a abajo, valor negativo, se mueve hacia a arriba
	     */
	    public double obtenerVelocidadVertical() {
            // TO-DO

            return this.velocidadVertical;//DEVUELVE EL VALOR DE VELOCIDAD VERTICAL
	    }

	    /**
	     * Dibuja la representacion grafica del MOB en la pantalla que se pasa como
	     * parametro
	     * 
	     * @param pantalla
	     *            Contexto grafico donde se dibuja el MOB
	     */
	    public void dibujar(Graphics pantalla) {
		    sprite.dibujar(pantalla, (int) x, (int) y);
	    }

	    /**
	     * Obtiene la posicion del MOB en el eje X
	     * 
	     * @return Posicion de la entidad en el eje X
	     */
	    public int obtenerPosicionX() {
            // TO-DO

            return this.x;//DEVUELVE VALOR ATRIBUTO X
	    }

	    /**
	     * Obtiene la posicion del MOB en el eje Y
	     * 
	     * @return Posicion de la entidad en el eje Y
	     */
	    public int obtenerPosicionY() {
            // TO-DO

            return this.y;//DEVUELVE VALOR ATRIBUTO Y
	    }

	    /**
	     * Devuelve el controlador de juego donde participa el MOB
	     * 
	     * @return controlador de juego donde participa el MOB
	     */
	    public ControladorJuego obtenerControladorJuego() {
		    return controladorJuego;
	    }

	    /**
	     * Establece el controlador de juego donde participa el MOB
	     * 
	     * @param controladorJuego
	     *            donde participa el MOB
	     */
	    public void establecerControladorJuego(ControladorJuego controladorJuego) {
		    this.controladorJuego = controladorJuego;
	    }

	    /**
	     * Comprueba si un MOB choca con otro
	     * 
	     * @param otro
	     *            MOB con el que comprobar si he chocado
	     * @return true si el MOB ha chocado con el otro MOB
	     */
	    public bool chocarCon(MOB otro) {
		    bool interseccion = false;
		    if (this != otro) {
			    /** Rectangulo que delimita al MOB actual */
			    Rectangle rectanguloMOB;
			    /** Rectangulo que delimita al MOB otro */
                Rectangle rectanguloOtroMOB;

			    rectanguloMOB = new Rectangle((int) x, (int) y, sprite.obtenerAncho(),
					    sprite.obtenerAlto());
			    rectanguloOtroMOB = new Rectangle((int) otro.x, (int) otro.y,
					    otro.sprite.obtenerAncho(), otro.sprite.obtenerAlto());

			    interseccion = rectanguloMOB.IntersectsWith(rectanguloOtroMOB);
		    }
		    return interseccion;
	    }

	    /**
	     * Gestiona lo que tiene que ocurrir cuando un MOB choca con otro MOB
	     * 
	     * @param colisionado
	     *            MOB con el que se colisiona
	     */
	    public abstract void colisionarCon(MOB colisionado);//METODO ABSTRACTO
    }
}
