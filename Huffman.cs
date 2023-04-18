using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static projects.Pixel;
using System.Collections;

namespace projects
{
    internal class Huffman
    {
        public Noeud root;
        public Dictionary<Color, int> arbre;

        public Huffman(Noeud root)
        {
            this.root = root;
            this.arbre = null;
        }

        /// <summary>
        /// constructor of the huffman class
        /// </summary>
        /// <param name="image">image a triller avec l'algorithme d huffman
        /// </param>
        public Huffman(Pixel[,] image)
        {
            arbre = new Dictionary<Color, int>();

            for(int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    Color c = new Color(image[i, j].R, image[i, j].G, image[i, j].B);

                    Color[] a = arbre.Keys.Where(o => (o.R == c.R && o.G == c.G && o.B == c.B)).ToArray();

                    if (a.Length == 0)
                    {
                        arbre.Add(new Color(c.R, c.G, c.G), 1);
                    }
                    else
                    {
                        arbre[a[0]] += 1;
                    }
                }
            }

            List<Noeud> nodes = new List<Noeud>();

            Console.WriteLine("Couleur & Fréquences :");

            foreach (Color key in arbre.Keys)
            {
                Console.WriteLine("(" + key.R + "," + key.G + "," + key.B + ") = " + arbre[key]);
            }

            foreach (Color key in arbre.Keys)
            {
                nodes.Add(new Noeud(key, arbre[key], null, null));
            }

            while (nodes.Count != 1)
            {
                Noeud n1 = MinFreqFromNodeList(nodes);
                nodes.Remove(n1);
                Noeud n2 = MinFreqFromNodeList(nodes);
                nodes.Remove(n2);

                Noeud join = new Noeud(new Color(-1, -1, -1), n1.frequence + n2.frequence, n1, n2);
                nodes.Add(join);
            }

            this.root = nodes[0];
        }

        /// <summary>
        /// DFS revisitée :-)
        /// </summary>
        /// <returns></returns>
        public List<bool> TreeEncode(Noeud node)
        {
            Console.WriteLine(node.ToString());

            List<bool> res = new List<bool>();

            if (node.pixel == null)
            {
                Console.WriteLine("A");

                List<bool> a = TreeEncode(node.noeudGauche);
                List<bool> b = TreeEncode(node.noeudDroit);

                res.Add(false);
                res.AddRange(a);
                res.AddRange(b);

                return res;
            }
            else
            {
                Console.WriteLine("B");


                res.Add(true);

                List<bool> a = new List<bool>();
                a.Add(true);


                res.AddRange(a);

                return res;
            }
        }

        /// <summary>
        /// Decode of the Huffman tree
        /// </summary>
        /// <param name="liste">list of all the bites</param>
        /// <returns></returns>
        public Noeud TreeDecode(BitArray liste)
        {
            Noeud root = null;

            if (liste.Count == 0)
            {
                return null;
            }
            if (liste[0] == true)
            {
                Color pix = new Color(GetByteFromBitArray(liste, 1), GetByteFromBitArray(liste, 9), GetByteFromBitArray(liste, 17));
                root = new Noeud(pix, 0, null, null);
            }
            else
            {
                Stack<Noeud> pile = new Stack<Noeud>();
                root = new Noeud(null, 0, null, null);
                pile.Push(root);

                for (int i = 1; i < liste.Length; i++)
                {
                    if (liste[i] == false)
                    {
                        pile.Push(new Noeud(null, 0, null, null));
                    }
                    else
                    {
                        Color pix = new Color(GetByteFromBitArray(liste, i + 1), GetByteFromBitArray(liste, i + 8), GetByteFromBitArray(liste, i + 16));

                        if (pile.Last().noeudGauche != null)
                        {
                            pile.Last().noeudDroit = new Noeud(pix, 0, null, null);

                            while (pile.Last().noeudGauche != null && pile.Last().noeudDroit != null)
                            {
                                Noeud temp = pile.Last();
                                pile.Pop();
                                if (pile.Last().noeudGauche != null)
                                {
                                    pile.Last().noeudDroit = temp;
                                }
                                else
                                {
                                    pile.Last().noeudGauche = temp;
                                }
                            }
                        }
                        else
                        {
                            pile.Last().noeudGauche = new Noeud(pix, 0, null, null);                           
                        }

                        i += 3 * 8;
                    }
                }
            }
            return root;
        }

        /// <summary>
        /// transform a bit array to a byte
        /// </summary>
        /// <param name="bitArray"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        static byte GetByteFromBitArray(BitArray bitArray, int start)
        {
            byte b = 0;

            if (bitArray.Count - start >= 8)
            {
                BitArray bits = new BitArray(8);

                for (int i = start; i < start + 8; i++)
                {
                    bits[i] = bitArray[start + i];
                }

                byte[] bytes = new byte[1];

                bits.CopyTo(bytes, 0);

                b = bytes[0];
            }

            return b;
        }

        /// <summary>
        /// Transform a byte list in a bit array
        /// </summary>
        /// <param name="b">byte array</param>
        /// <returns></returns>
        public static bool[] GetBoolFromByte(byte[] b)
        {
            bool[] result = new bool[8];
            BitArray bitArray = new BitArray(b);
            bitArray.CopyTo(result, 0);
            return result;
        }


        /// <summary>
        /// draw the current binary tree
        /// </summary>
        public void AfficherBinaireArbre()
        {
            Console.WriteLine("Couleur & Binaire");
            if (this.root != null && this.arbre != null)
            {
                foreach (Color key in this.arbre.Keys)
                {
                    List<bool> test = this.root.BinaireEnfants(key, new List<bool>());

                    if (test != null)
                    {
                        Console.Write("(" + key.R + "," + key.G + "," + key.B + ") = ");

                        foreach (bool bin in test)
                        {
                            if (bin)
                            {
                                Console.Write("1");
                            }
                            else
                            {
                                Console.Write("0");
                            }

                        }

                        Console.WriteLine();
                    }
                }
            }
        }


        /// <summary>
        /// get the minus frequency from a list of node
        /// </summary>
        /// <param name="liste">list of nods</param>
        /// <returns></returns>
        public static Noeud MinFreqFromNodeList(List<Noeud> liste)
        {
            Noeud node = null;
            int min;

            if (liste != null && liste.Count >= 0)
            {
                node = liste[0];
                min = node.frequence;

                foreach (Noeud nodes in liste)
                {
                    if (nodes.frequence < min)
                    {
                        min = nodes.frequence;
                        node = nodes;
                    }
                }
            }

            return node;
        }
    }


    /// <summary>
    /// Node class
    /// </summary>
    internal class Noeud
    {
        public Color ?pixel;
        public int frequence;
        public Noeud ?noeudDroit;
        public Noeud ?noeudGauche;

        /// <summary>
        /// constructor of node class
        /// </summary>
        /// <param name="pixel"></param>
        /// <param name="frequence"></param>
        /// <param name="noeudGauche"></param>
        /// <param name="?noeudDroit"></param>
        public Noeud(Color pixel, int frequence, Noeud? noeudGauche, Noeud ?noeudDroit)
        {
            if(pixel == null)
            {
                this.pixel = null;
            }
            else
            {
                this.pixel = new Color(pixel.R, pixel.G, pixel.B);
            }

            this.frequence = frequence;
            this.noeudDroit = noeudDroit;
            this.noeudGauche = noeudGauche;
        }


        /// <summary>
        /// get all binary code of the children nodes
        /// </summary>
        /// <param name="pixel"></param>
        /// <param name="?liste"></param>
        /// <returns></returns>
        public List<bool> BinaireEnfants(Color pixel, List<bool> ?liste = null)
        {
            // Return the list if the pixel researched correspond to this node
            if(this.pixel != null  && this.pixel.R == pixel.R && this.pixel.G == pixel.G && this.pixel.B == pixel.B)
            {
                return liste;
            }
            else
            {
                // Intial values
                List<bool> a = null;
                List<bool> b = null;

                // Verify if the searched pixel exists on the right child node (return != null)
                if (this.noeudDroit != null)
                {
                    a = noeudDroit.BinaireEnfants(pixel, liste);
                }

                // Verify if the serached pixel exists on the left child node (return != null)
                if (this.noeudGauche != null)
                {
                    b = noeudGauche.BinaireEnfants(pixel, liste);
                }

                // If the node exist on the left side, add the binary digit and return the list
                if(a == null && b!= null)
                {
                    b.Add(true);
                    return b;
                }

                // If the node exist on the right side, add the binary digit and return the list
                else if (a != null && b == null)
                {
                    a.Add(false);
                    return a;
                }

            }
            // If none of the baove, return null (not found)

            return null;

        }


        /// <summary>
        /// return the tostring of the node
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            string str = null;

            if(this.pixel != null)
            {
                str += "(" + this.pixel.R + " " + this.pixel.G + " " + this.pixel.B + ")\n";
            }
            else
            {
                str += "(null)\n";
            }

            str += "Freq : " + this.frequence + "\n";

            if(this.noeudGauche != null && this.noeudGauche.pixel != null)
            {
                str += "Node 1 : " + "(" + this.noeudGauche.pixel.R + " " + this.noeudGauche.pixel.G + " " + this.noeudGauche.pixel.B + ")\n";
            }
            else
            {
                str += "Node 1 : (null)\n";
            }

            if (this.noeudDroit != null && this.noeudDroit.pixel != null)
            {
                str += "Node 2 : " + "(" + this.noeudDroit.pixel.R + " " + this.noeudDroit.pixel.G + " " + this.noeudDroit.pixel.B + ")\n";
            }
            else
            {
                str += "Node 2 : (null)\n";
            }

            return str;
        }
    }
}
