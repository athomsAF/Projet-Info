using static System.Console;
using static System.BitConverter;
using System.Diagnostics;


namespace projects;
class Program
{
    static void Main(string[] args)
    {
        string[] files = {"Test","lac","lena","coco"};
        // create BitMap[] IMAGES for each file in files
        
        BitMap[] IMAGES = files.Select(f => new BitMap(f)).ToArray();
        //BitMap Test = new BitMap("./images/" + "Test.bmp");
        
        // WriteLine(string.Join(", ", IMAGES[imageChoice].Header)); //Select(x => x.ToString("x2")).ToArray()

        // WriteLine(string.Join(", ", IMAGES[imageChoice].ImageInfo));

        // WriteLine("Image Info : Height  - " + ConvertByteArrayToInt(IMAGES[imageChoice].Dimensions[0]) + " ; Width - " + ConvertByteArrayToInt(IMAGES[imageChoice].Dimensions[1]));
        // Process.Start(@".\imagesIN\Test.bmp");
        
        Pixel pixel = new Pixel((byte) 0,(byte) 0,(byte) 0);
    }
    
}
