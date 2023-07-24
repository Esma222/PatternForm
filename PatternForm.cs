using System;

namespace stringödeviHW3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool flag = true; int k = 0; int index;

            //text ve patternin içerebileceği karakterler için.
            string alphabettext = "ABCDEFGHIJKLMNOQPRSTUVWXYZabcdefghijklmnoqprstuvwxyz ,.";
            string alphabetpattern = "ABCDEFGHIJKLMNOQPRSTUVWXYZabcdefghijklmnoqprstuvwxyz-*";
            //kullanıcıdan text inputu istemek için
            Console.WriteLine("Please enter text:");
            string strtext = Console.ReadLine();


            //text içermemesi gereken bir karakter içeriyorsa yeniden text yazdırır.
            for (int i = 0; i < strtext.Length; i++)
            {
                while (!(alphabettext.Contains(strtext[i])))
                {

                    Console.WriteLine("Error!! Please enter new text:");
                    strtext = Console.ReadLine();
                    if (strtext == "")//texte hiçbir şey yazmayabilir.
                        break;
                    i = 0;
                }
            }
            //kullanıcıdan pattern inputu istemek için
            Console.WriteLine("Please enter pattern:");
            string strpattern = Console.ReadLine();

            //pattern içermemesi gereken bir karakter içeriyorsa  ya da - ve * karakterlerini aynı anda içeriyorsa yeniden pattern yazdırır.
            for (int j = 0; j < strpattern.Length; j++)
            {
                while (!(alphabetpattern.Contains(strpattern[j])) || ((strpattern.Contains("*") && strpattern.Contains("-"))))
                {

                    Console.WriteLine("Error!! Please enter new pattern:");
                    strpattern = Console.ReadLine();
                    if (strpattern == "")//patterne hiçbir şey yazmayabilir.
                        break;

                    j = 0;
                }
            }//textte sadece harfler ve boşluklar kalmalı, nokta ve virgülü silmek için
            strtext = " " + strtext + " ";
            strtext = strtext.Replace(",", " ");
            strtext = strtext.Replace(".", " ");
            //kelimeleri ayırıp arrayin içine atadım.
            string[] wordslistprint = strtext.Split(' ');//küçük harfe çevirmeden önceki hali output verirken kullanmak için
            strtext = strtext.Replace("I", "i");//tolower I yı küçük ı ya çevirmesin diye
            strtext = strtext.ToLower();
            string[] wordslist = strtext.Split(' ');//textteki tüm  kelimeler arrayin içinde.  Pattern ile karşılaştırmak için 
                                                    //pattern işlemleri
            strpattern = strpattern.Replace("I", "i");//tolower I yı küçük ı ya çevirmesin diye
            strpattern = strpattern.ToLower();
            string strpattern2 = strpattern.Replace("-", "");//- içermeyen pattern. Uzunluğunu kullanmak için
            string[] sameword = new string[wordslist.Length];//aynı kelimeleri yazdırmamak için output verilecek kelimeleri içerecek array
            string strpattern3 = strpattern.Replace("*", "");//* içermeyen pattern . Uzunluğunu kullanmak için
            string[] letterspattern = strpattern.Split('*');//patterni yıldız olan yerlerden bölüp indexlemek için.


            int counter = 0;


            // * içeren bir patternse

            if (strpattern.Contains("*"))
            {
                if (strpattern3.Length == 0)// pattern yalnızca * içeriyorsa değişkenin uzunluğu sıfıra eşittir
                {
                    for (int i = 0; i < wordslist.Length; i++)
                    {
                        for (int j = 0; j < wordslist[i].Length; j++)
                        {
                            if (wordslist[i][j] != ' ')// output olarak boşluk yazdırmamak için
                            {
                                //aynı kelimeleri yazdırmayı önler 
                                sameword[k] = wordslist[i];
                                for (int n = 0; n <= k; n++)
                                {
                                    for (int m = 0; m <= k; m++)
                                    {

                                        if (sameword[n] == sameword[m] && n != m)//aynı kelimeyse if in içine girer
                                        {
                                            flag = false;

                                            break;
                                        }

                                    }
                                }
                                if (flag == true)//aynı kelimeyse if in içine giremez
                                {
                                    Console.WriteLine(wordslistprint[i]);
                                    k++;
                                }
                                flag = true;

                            }
                        }
                    }
                }
                //hem yıldız hem harf içeren patternler için
                if (strpattern[0] != '*' && strpattern[strpattern.Length - 1] != '*')//başında ve sonunda yıldız yoksa
                {
                    for (int i = 0; i < wordslist.Length; i++)
                    {
                        flag = true;
                        int start = 0;
                        //kelimeler aynı şekilde başlayıp bitiyorsa
                        if (wordslist[i].StartsWith(letterspattern[0]) && wordslist[i].EndsWith(letterspattern[letterspattern.Length - 1]))
                        {


                            for (int j = 0; j < letterspattern.Length; j++)
                            {
                                //patterndeki yıldızlardan bölünen kısımları  sırasıyla içermiyorsa index -1 olur
                                //start ile sonraki kalıbı  önceki kalıbın bittiği yerden itibaren arar
                                index = wordslist[i].IndexOf(letterspattern[j], start);

                                if (index == -1)
                                {
                                    flag = false;
                                    break;
                                }
                                else
                                { start = index + letterspattern[j].Length; }

                            }

                            if (flag == true)
                            {
                                sameword[k] = wordslist[i];
                                for (int n = 0; n <= k; n++)
                                {
                                    for (int m = 0; m <= k; m++)
                                    {

                                        if (sameword[n] == sameword[m] && n != m)//aynı kelimeyi yazdırmayı önler
                                        {
                                            flag = false;

                                            break;
                                        }

                                    }
                                }
                                if (flag == true)
                                {
                                    Console.WriteLine(wordslistprint[i]);
                                    k++;
                                }
                                flag = true;
                            }
                        }
                    }
                }
                if (strpattern[0] != '*' && strpattern[strpattern.Length - 1] == '*')//başında yıldız yokken sonunda yıldız varsa
                {
                    for (int i = 0; i < wordslist.Length; i++)
                    {
                        flag = true;
                        int start = 0;
                        if (wordslist[i].StartsWith(letterspattern[0]))
                        {

                            for (int j = 0; j < letterspattern.Length; j++)
                            {
                                //patterndeki yıldızlardan bölünen kısımları  sırasıyla içermiyorsa index -1 olur
                                //start ile sonraki kalıbı  önceki kalıbın bittiği yerden itibaren arar
                                index = wordslist[i].IndexOf(letterspattern[j], start);
                                if (index == -1)
                                {
                                    flag = false;
                                    break;
                                }
                                else
                                { start = index + letterspattern[j].Length; }

                            }
                            if (flag == true)
                            {
                                sameword[k] = wordslist[i];
                                for (int n = 0; n <= k; n++)
                                {
                                    for (int m = 0; m <= k; m++)
                                    {

                                        if (sameword[n] == sameword[m] && n != m)//aynı kelimeyi yazdırmayı önler
                                        {
                                            flag = false;

                                            break;
                                        }

                                    }
                                }
                                if (flag == true)
                                {
                                    Console.WriteLine(wordslistprint[i]);
                                    k++;
                                }
                                flag = true;
                            }
                        }
                    }
                }
                if (strpattern[0] == '*' && strpattern[strpattern.Length - 1] != '*')//başında yıldız varken sonunda yıldız yoksa
                {
                    for (int i = 0; i < wordslist.Length; i++)
                    {
                        flag = true;
                        int start = 0;
                        if (wordslist[i].EndsWith(letterspattern[letterspattern.Length - 1]))
                        {
                            for (int j = 0; j < letterspattern.Length; j++)
                            {
                                //patterndeki yıldızlardan bölünen kısımları  sırasıyla içermiyorsa index -1 olur
                                //start ile sonraki kalıbı  önceki kalıbın bittiği yerden itibaren arar
                                index = wordslist[i].IndexOf(letterspattern[j], start);
                                if (index == -1)
                                {
                                    flag = false;
                                    break;
                                }
                                else
                                { start += letterspattern[j].Length; }

                            }
                            if (flag == true)
                            {
                                sameword[k] = wordslist[i];
                                for (int n = 0; n <= k; n++)
                                {
                                    for (int m = 0; m <= k; m++)
                                    {

                                        if (sameword[n] == sameword[m] && n != m)//aynı kelimeyi yazdırmayı önler
                                        {
                                            flag = false;

                                            break;
                                        }

                                    }
                                }
                                if (flag == true)
                                {
                                    Console.WriteLine(wordslistprint[i]);
                                    k++;
                                }
                                flag = true;
                            }
                        }
                    }
                }
                if (strpattern[0] == '*' && strpattern[strpattern.Length - 1] == '*') //başında ve sonunda yıldız varsa
                {
                    for (int i = 0; i < wordslist.Length; i++)
                    {
                        flag = true;
                        int start = 0;

                        for (int j = 0; j < letterspattern.Length; j++)
                        {
                            if (letterspattern[j] != "")
                            {  
                                //patterndeki yıldızlardan bölünen kısımları  sırasıyla içermiyorsa index -1 olur
                                //start ile sonraki kalıbı  önceki kalıbın bittiği yerden itibaren arar
                                index = wordslist[i].IndexOf(letterspattern[j], start);
                                if (index == -1)
                                {
                                    flag = false;
                                    break;
                                }
                                else
                                { start += letterspattern[j].Length; }
                            }
                        }
                        if (flag == true)
                        {
                            sameword[k] = wordslist[i];
                            for (int n = 0; n <= k; n++)
                            {
                                for (int m = 0; m <= k; m++)
                                {

                                    if (sameword[n] == sameword[m] && n != m)//aynı kelimeyi yazdırmayı önler
                                    {
                                        flag = false;

                                        break;
                                    }

                                }
                            }
                            if (flag == true)
                            {
                                Console.WriteLine(wordslistprint[i]);
                                k++;
                            }
                            flag = true;
                        }

                    }
                }

            }
            //- içeren ya da yalnızca harf içeren bir patternse
            else
            {
                for (int i = 0; i < wordslist.Length; i++)
                {
                    if (wordslist[i].Length == strpattern.Length)//  uzunlukları eşit olan kelimeler if in içine girer
                    {
                        for (int j = 0; j < strpattern.Length; j++)
                        {
                            if (strpattern[j] != '-' && wordslist[i][j] == strpattern[j])// aynı indexlerde aynı harfler varsa
                            {
                                counter++;// kaç indexin aynı olduğunu sayar
                                if (counter == strpattern2.Length)// - olmayanlar dışında tüm indexler eşitse  if in içine girer
                                {
                                    sameword[k] = wordslist[i];
                                    for (int n = 0; n <= k; n++)
                                    {
                                        for (int m = 0; m <= k; m++)
                                        {

                                            if (sameword[n] == sameword[m] && n != m)//aynı kelimeyi yazdırmayı önler
                                            {
                                                flag = false;

                                                break;
                                            }

                                        }
                                    }
                                    if (flag == true)
                                    {
                                        Console.WriteLine(wordslistprint[i]);
                                        k++;
                                    }
                                    flag = true;


                                }

                            }
                            else if (strpattern2.Length == 0)//yalnızca - içeriyorsa değişkenin uzunluğu 0 a eşittir.
                            {
                                if (strpattern[j] == '-')
                                {
                                    counter++;// kaç indexin aynı olduğunu sayar
                                    if (counter == strpattern.Length)
                                    {

                                        sameword[k] = wordslist[i];
                                        for (int n = 0; n <= k; n++)
                                        {
                                            for (int m = 0; m <= k; m++)
                                            {
                                               
                                                if (sameword[n] == sameword[m] && n != m)//aynı kelimeyi yazdırmayı önler
                                                {
                                                    flag = false;
                                                    break;
                                                }

                                            }
                                        }
                                        if (flag == true)
                                        {
                                            Console.WriteLine(wordslistprint[i]);
                                            k++;
                                        }
                                        flag = true;



                                    }
                                }

                            }
                        }
                        counter = 0;

                    }

                }

            }


        }
    }
}
