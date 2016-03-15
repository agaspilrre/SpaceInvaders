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

        private int aux = 0;//VARIABLE QUE UTILIZAMOS PARA CONTROLAR EL DESPLAZAMIENTO VERTICAL DEL ALIEN

	    /** Lista de MOB participantes en el juego */
	    private List<MOB> mobs=new List<MOB>(); //INICIALIZO LAS LISTAS
                                                /**
                                                 * Lista de MOB ha eliminar del juego, por ejemplo proyectil que desaparece
                                                 * por arriba o que colisiona con un alien.
                                                 */
        private List<MOB> mobsEliminados=new List<MOB>();

	    /**
	     * Inicializa los MOB participante en el juego (nava y alienigenas). Todos
	     * los MOB se almacenan en la lista de mobs. Se guarda también una
	     * referencia directa a la nave para poder interactuar con ella sin recorrer
	     * la lista de MOBS
	     * 
	     * @throws CargaSpriteException
	     *             se lanza si algun sprite de los MOB que participan en el
	     *             juego no se puede cargar correctamente
	     */
	    public void inicializar() {
            // resetear todos los elementos del controlador
           
           
           

            reset();//FUNCION DEL CONTROLADOR QUE RESETEA LAS LISTAS

            // crea la nave y la posiciona centrada en el la parte inferior de
            // pantalla, establece su controlador de juego y la añade a la lista de MOBs

            nave = new Nave("nave", 377, 500);//CREO UN OBJETO NAVE Y LE PASO EL NOMBRE DEL ARCHIVO QUE QUIERO QUE ME BUSQUE SU DIRECCION Y LAS COORDENADAS PARA SITUAR EL OBJETO EN LAS MISMAS DE LA PANTALLA
            nave.establecerControladorJuego(this);//ESTABLECER EL CONTROLADOR DE LA NAVE
            mobs.Add(nave);//AÑADO EL OBJETO NAVE A LA LISTA DE MOBS


            // crea un conjunto de alien (5 filas, 12 columnas)
            // se empieza en las coordenadas (100, 50) y se espacian entre ellos
            // tanto en vertical como en horizontal

            Alien [,] conjuntoAlien = new Alien[5, 12];//CREO UN ARRAY PARA GUARDAR OBJETOS TIPO ALIEN

            int aumentar = 0;//VARIABLE USADA PARA AUMENTAR EL ESPACIO DE LAS NAVES EN EJE X
            int aumentarY = 0;//VARIABLE PARA AUMENTAR EL ESPACIO ENTRE LAS NAVES EN EJE Y

            //RECORREMOS EL ARRAY DIMENSIONAL Y LE ASIGNAMOS UN OBJETO ALIEN EN CADA POSICION
            for(int i = 0; i < conjuntoAlien.GetLength(0); i++)
            {
               

                for (int j = 0; j < conjuntoAlien.GetLength(1); j++)
                {
                    conjuntoAlien[i,j] =new Alien("alien", 100+aumentar, 50+aumentarY,VELOCIDAD_INICIAL_ALIEN);//A LAS COORDENADAS LE VAMOS SUMANDO LAS VARIABLES AUMENTAR PARA QUE LOS ALIENS SE CREEN ESPACIADOS
                    conjuntoAlien[i, j].establecerControladorJuego(this);//ESTABLEZCO EL CONTROLADOR DE JUEGO EN LOS ALIENS

                    mobs.Add(conjuntoAlien[i,j]);//AÑADO LOS OBJETOS DEL ARRAY A LA LISTA DE MOBS
                    aumentar += 50;//AUMENTAMOS LA VARIABLE EN CADA VUELTA PARA ESPACIAR LOS ALIENES QUE SE CREEN EN EJE X

                    numeroAliens++;//INCREMENTO EL NUMERO DE ALIENS
                }

                aumentarY += 50;//AUMENTO LA COORDENADA EN Y PARA QUE HAYA ESPACIO EN VERTICAL ENTRE NAVES
                aumentar = 0;//VUELVO A PONER AUMENTARX A CERO PARA QUE CUANDO EMPIECE LA NUEVA FILA CON LA MISMA COORDENADA EN X

            }

            // Inicializar las variables gana y pierde

            gana=false;//AL REINICIAR UNA PARTIDA VUELVO A INICIALIZAR LAS VARIABLES GANA Y PIERDE
            pierde=false;
		    
        }

	    /**
	     * Realiza el movimiento de todos los MOBS que hay en la pantalla. Utiliza
	     * el tiempo transcurrido desde el ultimo movimiento. Desplazar los aliens en vertical
         * y buscar colisiones, haciendo uso de los metodos correspondientes.
	     * 
	     * @param tiempo
	     *            Cantidad de tiempo expresada en milisegundo
	     * @throws VelocidadErroneaException
	     *             se lanza se intenta mover un MOB de forma erronea. Un alien
	     *             solo se mueve hacia abajo en la pantalla en su desplazamiento
	     *             vertical
	     */
	    public void mover(long tiempo) {

            //RECORRO LA LISTA DE MOBS Y LLAMO A LA FUNCION MOVER DE CADA OBJETO EN LA POSICION ESPECIFICADA PARA ASIGNARLE EL MOVIMIENTO

            
            for(int i = 0; i < mobs.Count; i++)
            {
                mobs[i].mover(tiempo);
                
            }
           
            

            desplazarAlienVertical();//LLAMO CONSTANTEMENTE A LA FUNCION DESPLAZARALIENVERTICAL
            buscarColisiones();//LLAMO CONSTANTEMENTE A LA FUNCION BUSCARCOLISIONES
        }

	    /**
	     * Gestiona el desplazamiento vertical de los aliens. Si es necesario el
	     * desplazamiento vertical del alien, se cambia su velocidad vertical, en
	     * otro caso, si no es necesario el desplazamiento vertical, se establece la
	     * velocidad vertical a cero
	     * 
	     * @throws VelocidadErroneaException
	     *             se lanza se intenta mover un alienigena de forma erronea. Un
	     *             alien solo se mueve hacia abajo en la pantalla en su
	     *             desplazamiento vertical
	     */
	    private void desplazarAlienVertical() {

            // Si es necesario el desplazamiento vertical de los aliens, se buscan
            // los alien y se bajan. En otro casa se para el desplazamiento vertical
            // de los aliens. Si el alienigena llega a la parte inferior de la
            // pantalla, termina el juego y el jugador pierde

            //ESTA FUNCION ES LLAMADA DE CONTROLADOR JUEGO


            Alien alienAux;//VARIABLE TIPO ALIEN UTILIZADO PARA CONTROLAR LOS DESPLAZAMIENTOS DE LOS ALIEN

            //SI DESPLAZAMIENTO VERTICAL ES IGUAL A TRUE Y LA VARIABLE AUX ES IGUAL A CERO PASA DENTRO
            if (desplazamientoAlienVerticalNecesario && aux == 0)
            {
                //RECORRO LA LISTA
                for (int i = 0; i < mobs.Count; i++)
                {
                    alienAux = mobs[i] as Alien;//SI EL OBJETO DE LA POSICION I ES UN ALIEN LO GUARDA EN LA VARIABLE SI NO GUARDA UN NULL

                    if (alienAux != null)
                    {
                        alienAux.bajar(VELOCIDAD_INICIAL_ALIEN);//LLAMO A LA FUNCION BAJAR DE LA CLASE ALIEN Y LE PASO LA VARIABLE DE VELOCIDAD INICIAL ALIEN
                        int posicionY = alienAux.obtenerPosicionY();//LLAMO A LA FUNCION DE OBTENER Y LA GUARDO EN UNA VARIABLE INT

                        if (posicionY >= 500)
                        {
                            establecerHaPerdido(true);//SI LA POSICION Y DEL ALIEN PASA DE 500 LLAMA A LA FUNCION ESTABLECERHAPERDIDO
                        }
                     
                    }

                  
      

                }

                aux++;//INCREMENTO AUX PARA QUE NO VUELVA A PASAR A ESTA MISMA CONDICION
            }

            

            else if (aux<7)
            {
                aux++;//VY INCREMENTANDO AUX MIENTRAS SEA MENOR DE QUE EL NUMERO DE LA CONDICION ESTO ES PARA CONTROLAR EL TIEMPO QUE ESTA BAJANDO LAS NAVES UNA VEZ QUE AUX SE MAOR QUE EL NUMERO DE LA CONDICION SE METERIA EN EL ELSE
            }
            else
            {
                aux = 0;//PONGO AUX A CERO PARA PODER VOLVER A ENTRAR EN EL PRIMER IF CUANDO LOS ALIENS VUELVAN A CHOCAR
                establecerDesplazamientoAlienVerticalNecesario(false);//LLAMO ESTA FUNCION PARA VOLVERLA PONER A FALSE YA QUE DESDE OTRA FUNCION LA PONDREMOS A TRUE CUANDO SE CUMPLA DETERMINADA CONDICION

                //RECORRO LA LISTA DE MOBS
                for(int i = 0; i < mobs.Count; i++)
                {
                    alienAux = mobs[i] as Alien;//SI EL OBJETO EN LA POSICION Y ES UN ALIEN LO GUARDA, SI NO GUARDA UN NULL

                    if (alienAux != null)
                    {
                        alienAux.establecerVelocidadVertical(0);//SI ES DISTINTO DE NULL ESTABLECE LA VELOCIDAD VERTICAL DEL ALIEN A CERO
                    }

                    
                }
            }
            

			
        }

	    /**
	     * Elimina un mob del juego. No se movera ni se pintara
         * añadiendolo a la lista de mobsEliminados
	     * 
	     * @param mob
	     *            El mob a eliminar
	     */
	    public void eliminarMOB(MOB mob) {

         
                    mobsEliminados.Add(mob);//SI EL PARAMETRO QUE RECIBE ESTA FUNCION ES IGUAL A AL OBJETO DE LA POSICION AÑADO ESTE OBJETO A LA LISTA DE ELIMINADOS
                    mobs.Remove(mob);//Y BORRO EL OBJETO DE LA LISTA DE MOBS
          
		    
	    }

	    /**
	     * Notifica que un alien ha sido abatido por un proyectil. Los aliens
	     * incrementan su velocidad un 2%
	     */
	    public void notificarAlienAbatido() {
            // decremento el numero de alienigenas
            numeroAliens--;

            // si el numero de alienigenas es cero, el jugador gana

            if (numeroAliens == 0)
            {
                establecerHaGanado(true);

            }
            // en otro caso si queda algun alienigena, se incrementa la velocidad de todos
            // los alienigenas restantes en un 2%
            
            else
            {
                for(int i=0;i<mobs.Count;i++)
                {
                    if(mobs[i] is Alien)
                    {
                       
                        //SI MOBS EN LA POSICION I ES UN ALIEN SE METE Y LE ESTABLECEMOS COMO VELOCIDAD HORIZONTAL LA VELOCIDAD QUE TENGA MULTIPLICADA POR UN DOS POR CIENTO
                        mobs[i].establecerVelocidadHorizontal(mobs[i].obtenerVelocidadHorizontal()*1.02);

                    }
                    
                }

            }
                
			
	    }

	    /**
	     * Busca las colisione que se producen entre todos los mobs del que hay
	     * actualmente en el juego. Busqueda mediante fuerza bruta, se buscan
	     * colisiones de todos los mobs con todos los mobs. Hacer uso de los metodos chocarCon
         * y colisionarCon
	     */
	    private  void buscarColisiones() {

            //BUCLE ANIDADO NECESARIO PARA COMPROBAR LOS OBJETOS DE POSICIONES CON POSICIONES POSTERIORES
            for(int i = 0; i < mobs.Count; i++)
            {
                for(int j = i+1; j < mobs.Count; j++)
                {
                    

                  //ESTE IF ESTA PARA EVITAR QUE LOS ALIENS DETECTEN UNA COLISION ENTRE ELLOS MISMO PARA ELLO CONTEMPLAMOS TODAS LAS DEMAS POSIBILIDADES Y EN ESTAS LLAMAMOS A LA FUNCION DE COLISIONAR
                     if((mobs[i] is Nave && mobs[j] is Alien)  || (mobs[i] is Alien && mobs[j] is Proyectil))
                        {
                             mobs[i].colisionarCon(mobs[j]);//EN CADA POSICION EL OBJETO QUE ESTE CONTENIDO LLAMARA A SU METODO COLISIONAR CON Y LE PASARA COMO PARAMETRO LA POSICION SIGUIENTE

                                //COMPROBAR LOS CHOCAR CON LO HAGO EN LAS FUNCIONES DE COLISIONARCON
                            


                        }
                        
                        
                    

                }
               
                    
            }
            
            
		   
	    }

	    /**
	     * Dibuja los mobs en la pantalla que se pasa como parametro. Una vez
	     * dibujado se actualiza la lista de mobs eliminado aquellos que en la
	     * siguiente representacion no deban aparecer
	     * 
	     * @param pantalla
	     *            pantalla donde se dibujar el sprite que representa a cada mob
	     */
	    public void representar(Graphics pantalla) {


            for(int i = 0; i < mobs.Count; i++)
            {
                mobs[i].dibujar(pantalla);//RECORRO LA LISTA Y LLAMO AL METODO DIBUJAR PARA DIBUJAR LOS OBJETOS EN PANTALLA

            }

		    
	    }

	   
	   
    }
}
