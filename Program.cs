class Program
{
    static void Main(string[] args)
    {

        //var sourcePath = @"C:\Users\Aliye_du26\Desktop\1\New Text Document.txt";
        // var destPath = @"C:\Users\Aliye_du26\Desktop\2\New Text Document.txt";


        static void download(string sourcePath, string destPath, char key)
        {

            using (var source = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (var dest = new FileStream(destPath, FileMode.Create, FileAccess.Write))
                {


                    int len = 2;
                    var bytes = new byte[len];
                    var fileSize = source.Length;

                    do
                    {

                        len = source.Read(bytes, 0, len);
                        var e = Convert.ToChar(bytes);
                        var enc = e ^ key;
                        var newBytes = new byte[len];
                        newBytes[0] = (byte)enc;
                        //dest.Write(bytes, 0, len);
                        dest.Write(newBytes, 0, len);

                        fileSize -= len;

                    } while (len > 0);
                }
            }
        }

        Console.WriteLine("Enter Source Path:");
        var sourcePath = Console.ReadLine();
        Console.WriteLine("Enter Destination Path:");
        var destPath = Console.ReadLine();
        Console.WriteLine("Enter Key:");
        var key = Convert.ToChar(Console.ReadLine());

        ThreadPool.QueueUserWorkItem(o => {
            download(sourcePath, destPath, key);
            Console.WriteLine("END");
        });
        Console.ReadKey();



    }
}
