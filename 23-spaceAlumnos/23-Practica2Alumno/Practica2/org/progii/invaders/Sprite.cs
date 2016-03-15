using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace org.poo.invaders
{
    /* 
     * Representa una imagen a mostrar en la pantalla. El sprite no contiene
     * informacion referente al estado del MOB que represnta Un mismos sprite puede
     * ser utilizado simultaneamente por varios MOB
     * 
     */
    public class Sprite
    {
        private Image imagen;

        /**
         * Crea un nuevo sprite
         * 
         * @param imagen
         *            Imagen asociada al sprite
         */
        public Sprite(Image imagen)
        {
            this.imagen = imagen;
        }

        /**
         * Obtiene el ancho del sprite
         * 
         * @return Ancho en pixeles del sprite
         */
        public int obtenerAncho()
        {
            
            return imagen.Width;
        }

        /**
         * Obtiene el alto del sprite
         * 
         * @return Alto en pixeles del sprite
         */
        public int obtenerAlto()
        {
            return imagen.Height;
        }

        /**
         * Dibuja el sprite en el contexto grafico proporcionado, en la posicion
         * indicada
         * 
         * @param pantalla
         *            Contexto grafico donde se representara el sprite
         * @param x
         *            Coordenada x dentro del contexto grafico donde se dibuja el
         *            sprite
         * @param y
         *            Coordenada y dentro del contexto grafico donde se dibuja el
         *            sprite
         */
        public void dibujar(Graphics pantalla, int x, int y)
        {
            pantalla.DrawImage(imagen, x, y);
        }
    }
}
