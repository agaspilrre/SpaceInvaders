using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.IO;
using org.poo.invaders.exceptions;
using System.Resources;

namespace org.poo.invaders
{
    /**
     * 
     * Representa un gestor de recursos para el juego, en este caso se encarga de
     * construir los sprites que se utilizan en el juego.
     * <p>
     * [singleton]
     * <p>
     */
    public class SpriteFactory
    {
        /** Unica instancia de la clase */
	private static SpriteFactory instacia = new SpriteFactory();

	/**
	 * Devuelve la única instancia de la clase
	 * 
	 * @return La instancia de la clase
	 */
	public static SpriteFactory obtenerSpriteFactory() {
		return instacia;
	}

	/** Almacen de sprites */
	private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

	/**
	 * Devuelve un sprite del almacen
	 * 
	 * @param rutaImagen
	 *            Ruta al fichero que tiene la representacion del sprite
	 * @return Una instancia de sprite con la imagen cargada a partir de
	 *         rutaImagen
	 * @throws CargaSpriteException
	 *             se lanza si la ruta de la imagen que se pasa como parametro
	 *             es nula o bien hay algún problema al cargar el fichero que
	 *             contiene la imagen que representa el sprite
	 * 
	 */
	public Sprite obtenerSprite(string rutaImagen) {
		// Si el sprite esta en el almacen
		// se devuelve el sprite que esta en el almacen
        Sprite sprite;
		if (sprites.TryGetValue(rutaImagen, out sprite)) {
			return sprite;
		}

		// Si no esta en el almacen, se carga la imagen y se genera el sprite
		// correspondiente
		Bitmap ficheroImagen = null;

		try {

            //ficheroImagen = new Bitmap(rutaImagen);


           // ficheroImagen = Practica2.Properties.Resources.nave;

            ficheroImagen = (Bitmap)Practica2.Properties.Resources.ResourceManager.GetObject(rutaImagen, Practica2.Properties.Resources.Culture);
		} catch (FileNotFoundException e) {
			throw new CargaSpriteException("Fallo al cargar la imagen: "
					+ rutaImagen);
		}
        

		// se crear el sprite con el tamaño adecuado
        //Graphics gc = Graphics
        //GraphicsConfiguration graphicsConfig = GraphicsEnvironment
        //        .getLocalGraphicsEnvironment().getDefaultScreenDevice()
        //        .getDefaultConfiguration();
        //Image image = graphicsConfig.createCompatibleImage(
        //        ficheroImagen.getWidth(), ficheroImagen.getHeight(),
        //        Transparency.BITMASK);

        //// se dibuja la imagen
        //image.getGraphics().drawImage(ficheroImagen, 0, 0, null);

		// se crea el spite y se guarda en el almacen
        sprite = new Sprite(ficheroImagen);
		sprites.Add(rutaImagen, sprite);

		return sprite;
	}

    }
}
