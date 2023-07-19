using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RecommendSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //чтение из файла
            string readPath = @"C:\Work\User-Rate (7).csv";
            string[,] general_mas = new string[150,7]; //150 массивов по 6 эл-тов
            string[] file_mas_split = null;
            string[] file_mas = File.ReadAllLines(readPath);


            for (int i = 0; i < file_mas.Length; i++)
            {
               
                    if (!String.IsNullOrEmpty(file_mas[i]))
                    {
                        file_mas_split = file_mas[i].Split(',');

                    }
                    for (int m = 0; m < 6; m++)
                    {
                        general_mas[i, m] = file_mas_split[m];
                        
                    }
               
            }

            
             /* * ВЫВОД ОБЩЕГО МАССИВА (считанного из файла) В КОНСОЛЬ 20 эл-тов**
            //WriteLine(general_mas[2, 5]); 
            Console.WriteLine("GENERAL_MAS ");
          for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Строка {0} ", i);
                for (int j = 0; j < 6; j++)
                {
                    Console.WriteLine(general_mas[i,j]);
                }
            }
            */  
              

            //создание массива препочтений заданного пользователя
          string[,] prefer_mas = new string[20, 6];//массив предпочтений польз-ля
        
          int[] index = new int[22];
          int k=0;
          Console.Write("Введите логин пользователя для создания персональной рекомендации: ");
          string login = "\"" + Console.ReadLine() + "\"";

          try
          {
              for (int i = 0; i < 150; i++)
              {
                  if (general_mas[i, 0] == login)
                  {
                      index[k] = i;
                      k++;
                  }
              }

              for (int i = 0; i < 20; i++)
              {
                  for (int j = 0; j < 6; j++)
                  {
                      if (!(i > 0 && index[i] == 0))
                      {
                          prefer_mas[i, j] = general_mas[index[i], j];
                      }
                  }
              }

          }catch
          {
              Console.WriteLine("Пользователь не найден");
          }
            if (k == 0)
            {
                Console.WriteLine("Пользователь не найден");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
              // * ВЫВОД МАССИВА ПРЕДПОЧТЕНИЙ В КОНСОЛЬ*
                   Console.WriteLine("mas_prefer: ");
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("Строка {0} ", i);
                for (int j = 0; j < 6; j++)
                {
                    Console.WriteLine(prefer_mas[i, j]);
                }
            }
               

            //создание временного массива для пользователя, с которым сравнивают, и создание вектора для Расстояние Хемминга
              string[,] prefer_mas_temp = new string[20, 6];//массив предпочтений другого польз-ля
              int[] index_temp = new int[150];
              int k_temp = 0;
              string login_temp = null;
              string[,] Hem_sum = new string[2, 150];
            
            int j_sum=0;


              int count=0;
              

              for (int g = 0; g < 150; g++)
              {
                  if (general_mas[g, 0] != login && general_mas[g, 0] != null && general_mas[g, 6] != "flag")
                  {
                      login_temp = general_mas[g, 0];                      
                  }
              //}//вынести на более большой цикл, чтобы делалось пока не создаст массив с оценками

                  if (login_temp != null) //СОЗДАНИЕ ВРЕМЕННОГО МАССИВА
                  {
                      
                      for (int i = 0; i < 150; i++)
                      {
                          if ((general_mas[i, 0] == login_temp) && general_mas[i, 0] != null)
                          {
                              index_temp[k_temp] = i;
                              k_temp++;
                              
                          }
                      }
                     // k_temp = 0;
                     // login_temp = null;

                      for (int i = 0; i < 20; i++)
                      {
                          for (int j = 0; j < 6; j++)
                          {
                              if (!(i > 0 && index_temp[i] == 0))
                              {
                                  prefer_mas_temp[i, j] = general_mas[index_temp[i], j];
                                  general_mas[index_temp[i], 6] = "flag"; // flag, что логин уже был просмотрен
                              }
                          }
                      }
                      count++;


                    
                    // ВЫВОД ВРЕМЕННОГО МАССИВА
                     /*  Console.WriteLine("{0} mas_prefer_temp: ", count);
                      for (int i = 0; i < 10; i++)
                      {
                          Console.WriteLine("Строка {0} ", i);
                          for (int j = 0; j < 6; j++)
                          {
                              Console.WriteLine(prefer_mas_temp[i, j]);
                          }
                      }
                      * */
                     
                     // Console.ReadKey();
                       
                      
                     
                      //создание вектора для Расстояние Хемминга
                      int sum = 0;
                      

                      for (int i = 0; i < 20; i++)
                      {
                          for (int i_temp = 0; i_temp < 20; i_temp++)
                          {


                              if ((prefer_mas[i, 2] == prefer_mas_temp[i_temp, 2] && prefer_mas[i, 2] != "" && prefer_mas_temp[i_temp, 2] != "" && prefer_mas[i, 2] != null && prefer_mas_temp[i_temp, 2] != null) && (Math.Abs(Convert.ToInt32(prefer_mas[i, 5]) - Convert.ToInt32(prefer_mas_temp[i_temp, 5])) <= 1))
                              {
                                  sum++;

                                  // Console.WriteLine("совпал отель");
                              }
                              if ((prefer_mas[i, 3] == prefer_mas_temp[i_temp, 3] && prefer_mas[i, 3] != "" && prefer_mas_temp[i_temp, 3] != "" && prefer_mas[i, 3] != null && prefer_mas_temp[i_temp, 3] != null) && (Math.Abs(Convert.ToInt32(prefer_mas[i, 5]) - Convert.ToInt32(prefer_mas_temp[i_temp, 5])) <= 1))
                              {
                                  sum++;

                                  //Console.WriteLine("совпало кафе");
                              }
                             
                              // && (Convert.ToInt32(prefer_mas[i, 5]) == Convert.ToInt32(prefer_mas_temp[i_temp, 5]) || Convert.ToInt32(prefer_mas[i, 5]) == (Convert.ToInt32(prefer_mas_temp[i_temp, 5]) + 1) || Convert.ToInt32(prefer_mas[i, 5]) == (Convert.ToInt32(prefer_mas_temp[i_temp, 5]) - 1))
                              if ((prefer_mas[i, 4] == prefer_mas_temp[i_temp, 4] && prefer_mas[i, 4] != "" && prefer_mas_temp[i_temp, 4] != "" && prefer_mas[i, 4] != null && prefer_mas_temp[i_temp, 4] != null) && (Math.Abs(Convert.ToInt32(prefer_mas[i, 5]) - Convert.ToInt32(prefer_mas_temp[i_temp, 5])) <=1))
                              {
                                  sum++;
                                  
                                /*Console.WriteLine("совпала достоприм, i = {0}, i_temp= {1}, id= {2}, login {3}", i, i_temp, prefer_mas_temp[i_temp, 4], login_temp);
                                Console.ReadKey();
                                 * */
                              }

                          }
                      }

                      if (login_temp != "\"Title\"")
                      {
                          Hem_sum[0, j_sum] = login_temp;
                          Hem_sum[1, j_sum] = sum.ToString();
                          j_sum++;
                      }


                      for (int i = 0; i < 20; i++)
                      {
                          for (int j = 0; j < 6; j++)
                          {

                              prefer_mas_temp[i, j] = null;

                          }
                      }

                      for (int i = 0; i < 20; i++)
                      {
                          index_temp[i] = 0;
                      }

                      
                      k_temp = 0;
                      login_temp = null;
                  }//конец прохода по 1 пользователю
                  
              
              } // конец цикла прохода по всем сравниваемым пользователям  

            /* ВЫВОД ВЕКТОРА ХЕММИНГА
            
              for (int i = 0; i < 2; i++)
              {
                  for (int j = 0; j < 25; j++) //150
                  {

                      Console.WriteLine(Hem_sum[i, j]);

                  }
              }
             * */
             

            //сортировка вектора Хемминга

             Boolean f = false;
              do
              {
                  f = false;
                  for (int j = 0; j < 149; j++)
                  {

                      if (Convert.ToInt32(Hem_sum[1, j]) < Convert.ToInt32(Hem_sum[1, j + 1]))
                      {
                          string[,] tmp = new string[2, 2];
                          tmp[0, 0] = Hem_sum[0, j];
                          tmp[1, 0] = Hem_sum[1, j];
                          Hem_sum[0, j] = Hem_sum[0, j + 1];
                          Hem_sum[1, j] = Hem_sum[1, j + 1];
                          Hem_sum[0, j + 1] = tmp[0, 0];
                          Hem_sum[1, j + 1] = tmp[1, 0];
                          f = true;
                      }

                  }

              } while (f);

            /*
                 Console.WriteLine("SORT Хемминга");
                 for (int i = 0; i < 2; i++)
                 {
                     for (int j = 0; j < 25; j++)//150
                     {

                         Console.WriteLine(Hem_sum[i, j]);

                     }
                 }
             */

              /*
               * Console.WriteLine("mas_prefer_temp: ");
              for (int i = 0; i < 20; i++)
              {
                  Console.WriteLine("Строка {0} ", i);
                  for (int j = 0; j < 6; j++)
                  {
                      Console.WriteLine(prefer_mas_temp[i, j]);
                  }
              }
            */
         /*
      //СОСТАВЛЕНИЕ РЕКОМЕНДАЦИИ
              int count_hotel = 0;
              int count_food = 0;
              int count_showpl = 0;
              string[,] recommend_mas = new string[10,5];
              int[] index_recom = new int[150];
              int i_rec = 0;

              for (int i = 0; i < 20; i++)
              {
                  for (int g = 0; g < 150; g++)
                  {
                      for (int j = 2; j < 5; j++)
                        {
                            if (general_mas[g, j] == prefer_mas[i, j] && general_mas[g, j] != "" && general_mas[g, j] != null && prefer_mas[i, j] != "" && prefer_mas[i, j] != null)
                             {
                                 general_mas[g, 6] = "flag_rec"; // flag, что место уже было просмотрено
                             }
                        }
                  }
              }

              for (int i = 0; i < 150; i++)
              {
                  Console.WriteLine("Строка {0} ", i);
                  for (int j = 0; j < 7; j++)
                  {
                      if(general_mas[i, 6] == "flag_rec")
                      Console.WriteLine(general_mas[i, j]);
                  }
              }
              
            do
               {
                  for (int j = 0; j < 150; j++)
                  {
                      
                      for (int g = 0; g < 150; g++)
                      {

                          if (Hem_sum[0, j] == general_mas[g, 0] && (count_hotel + count_food + count_showpl < 10))
                          {
                              
                              
                                for (int i = 0; i < 20; i++)
                              {
                                  if (prefer_mas[i, 2] != general_mas[g, 2] && prefer_mas[i, 2] != "" && general_mas[g, 2] != "" && prefer_mas[i, 2] != null && general_mas[g, 2] != null && Convert.ToInt32(general_mas[g, 5]) > 3 && general_mas[g, 6] != "flag_rec")
                                  {
                                      index_recom[i_rec] = g;
                                      general_mas[g, 6] = "flag_rec"; // flag, что место уже было просмотрено
                                      i_rec++;
                                      count_hotel++;                                      
                                  }

                                  if (prefer_mas[i, 3] != general_mas[g, 3] && prefer_mas[i, 3] != "" && general_mas[g, 3] != "" && prefer_mas[i, 3] != null && general_mas[g, 3] != null && Convert.ToInt32(general_mas[g, 5]) > 3 && general_mas[g, 6] != "flag_rec")
                                  {
                                      index_recom[i_rec] = g;
                                      general_mas[g, 6] = "flag_rec"; // flag, что место уже было просмотрено
                                      i_rec++;
                                      count_food++;
                                  }

                                  if (prefer_mas[i, 4] != general_mas[g, 4] && prefer_mas[i, 4] != "" && general_mas[g, 4] != "" && prefer_mas[i, 4] != null && general_mas[g, 4] != null && Convert.ToInt32(general_mas[g, 5]) > 3 && general_mas[g, 6] != "flag_rec")
                                  {
                                      index_recom[i_rec] = g;
                                      general_mas[g, 6] = "flag_rec"; // flag, что место уже было просмотрено
                                      i_rec++;
                                      count_showpl++;
                                  }
                                  


                              }//конец прохода по prefer_mas  
                               
                          }
                        
                      }//конец прохода по general_mas для 1 польз-ля из Hem_Sum
                         
                      //i_rec = 0;
                         
                  }//конец прохода по Hem_Sum
               } while (count_hotel + count_food + count_showpl < 10);

                /*  //запись в recommend_mas c id польз-лей, от которых поступила рекомендация
                  Console.WriteLine(" general_mas[index_recom[i], j] ");
                  for (int i = 0; i < 9; i++)
                  {
                      for (int j = 0; j < 5; j++)
                      {

                          recommend_mas[i, j] = general_mas[index_recom[i], j];
                          Console.WriteLine(general_mas[index_recom[i], j]);
                      }
                      
                  }
                 * */

              string[,] recommend_mas = new string[10, 5];
              int count_hotel = 0;
              int count_food = 0;
              int count_showpl = 0;
              int count_hem = 0;
              int[] index_recom = new int[150];
              int i_rec = 0;
              string[] hotel_rec = new string[10];
              string[] food_rec = new string[10];
              string[] showpl_rec = new string[10];

              do
              {

                  for (int i = 0; i < 20; i++)
                      {
                          for (int j = 0; j < 6; j++)
                          {
                              prefer_mas_temp[i, j] = null;
                          }
                      }

                  if (Hem_sum[0, count_hem] != null) //СОЗДАНИЕ ВРЕМЕННОГО МАССИВА
                  {

                      for (int i = 0; i < 150; i++)
                      {
                          if ((general_mas[i, 0] == Hem_sum[0, count_hem]) && general_mas[i, 0] != null)
                          {
                              index_recom[i_rec] = i;
                              i_rec++;

                          }
                      }


                      for (int i = 0; i < 20; i++)
                      {
                          for (int j = 0; j < 6; j++)
                          {
                              if (!(i > 0 && index_recom[i] == 0))
                              {
                                  prefer_mas_temp[i, j] = general_mas[index_recom[i], j];
                              }

                          }
                      }
                  }
                  ///////////////////////
                  string[] prefer_mas_string = new string[130];
                  int count_string = 0;
                  for (int i = 0; i < 20; i++)
                  {
                      for (int j = 0; j < 6; j++)
                      {
                          prefer_mas_string[count_string] = prefer_mas[i, j]; //запись в одномерный массив
                          count_string++;

                      }
                  }
                  



                  for (int i = 0; i < 20; i++)
                  {
                      for (int i_temp = 0; i_temp < 20; i_temp++)
                      {


                          if ((prefer_mas[i, 2] != prefer_mas_temp[i_temp, 2] && prefer_mas[i, 2] != "" && prefer_mas_temp[i_temp, 2] != "" && prefer_mas[i, 2] != null && prefer_mas_temp[i_temp, 2] != null) && Convert.ToInt32(prefer_mas_temp[i, 5])>3 )
                          {
                              if (!(prefer_mas_string.Contains(prefer_mas_temp[i_temp, 2])))
                              {
                                  if (!(hotel_rec.Contains(prefer_mas_temp[i_temp, 2])))
                                  {
                                      hotel_rec[count_hotel] = prefer_mas_temp[i_temp, 2];
                                      count_hotel++;
                                  }
                              }
                                                           
                          }
                          if ((prefer_mas[i, 3] != prefer_mas_temp[i_temp, 3] && prefer_mas[i, 3] != "" && prefer_mas_temp[i_temp, 3] != "" && prefer_mas[i, 3] != null && prefer_mas_temp[i_temp, 3] != null) && Convert.ToInt32(prefer_mas_temp[i, 5]) > 3)
                          {
                              if (!(prefer_mas_string.Contains(prefer_mas_temp[i_temp, 3])))
                              {
                                  if (!(food_rec.Contains(prefer_mas_temp[i_temp, 3])))
                                  {
                                      food_rec[count_food] = prefer_mas_temp[i_temp, 3];
                                      count_food++;
                                  }
                              }
                          }

                          if ((prefer_mas[i, 4] != prefer_mas_temp[i_temp, 4] && prefer_mas[i, 4] != "" && prefer_mas_temp[i_temp, 4] != "" && prefer_mas[i, 4] != null && prefer_mas_temp[i_temp, 4] != null) && Convert.ToInt32(prefer_mas_temp[i, 5]) > 3)
                          {
                              if (!(prefer_mas_string.Contains(prefer_mas_temp[i_temp, 4])))
                              {
                                  if (!(showpl_rec.Contains(prefer_mas_temp[i_temp, 4])))
                                  {
                                      showpl_rec[count_showpl] = prefer_mas_temp[i_temp, 4];
                                      count_showpl++;
                                  }
                              }
                          }

                      }
                  }
                  ////////////////////////
                  count_hem++;

              } while ((count_showpl +  count_food + count_hotel < 10) && Hem_sum[0,count_hem] != null);


            
                  //запись в recommend_mas c id польз-ля, для которого поступила рекомендация
            count_showpl = 0;
            count_food = 0;
            count_hotel = 0;
            
                  Console.WriteLine(" general_mas[index_recom[i], j] ");
                  recommend_mas[0, 0] = "\"Title\"";
                  recommend_mas[0, 1] = "\"User\"";
                  recommend_mas[0, 2] = "\"Hotel\"";
                  recommend_mas[0, 3] = "\"Food\"";
                  recommend_mas[0, 4] = "\"Showplace\"";
                  for (int i = 1; i < 10; i++)
                  {                  
                      recommend_mas[i, 0] = login;
                      recommend_mas[i, 1] = prefer_mas[0,1];                   
                      
                  }
            
                  for (int i = 1; i < 4; i++)
                  {
                      recommend_mas[i, 2] = hotel_rec[count_hotel];
                      count_hotel++;
                  }
                  //for (int i = 4; i < 7; i++)
                  for (int i = 1; i < 4; i++)
                  {
                      recommend_mas[i, 3] = food_rec[count_food];
                      count_food++;
                  }
                  //for (int i = 7; i < 10; i++)
                  for (int i = 1; i < 4; i++)
                  {
                      recommend_mas[i, 4] = showpl_rec[count_showpl];
                      count_showpl++;
                  }
                  

                  Console.WriteLine(" recommend_mas[index_recom[i], j] ");
                  for (int i = 0; i < 10; i++)
                  {
                      for (int j = 0; j < 5; j++)
                      {

                          
                          Console.WriteLine(recommend_mas[i, j]);
                      }

                  }
              

            //запись в файл
          string writePath = @"C:\Work\Recom.csv";
          //StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default);
          string file_join = null;
          string file_join_final = null;
          string[] file_join_mas = new string[5];
          string[] file_join_general = new string[10];

          for (int i = 0; i < 10; i++)
          {
              for (int j = 0; j < 5; j++)
              {
                  file_join_mas[j] = recommend_mas[i, j]; //то что записываем
                
              }
              
              file_join = string.Join(",", file_join_mas);
              if (!String.IsNullOrEmpty(file_join) && file_join !=",,,,,")
              {
                  file_join_general[i] = file_join;
              }
          }

          file_join_final = string.Join("\n", file_join_general);
          /*
             string writePath = @"C:\Work\Recom.csv";
        //StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default);
        string file_join = null;
        string file_join_final = null;
        string[] file_join_mas = new string[6];
        string[] file_join_general = new string[150];

        for (int i = 0; i < 150; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                file_join_mas[j] = general_mas[i, j]; //то что записываем
                
            }
              
            file_join = string.Join(",", file_join_mas);
            if (!String.IsNullOrEmpty(file_join) && file_join !=",,,,,")
            {
                file_join_general[i] = file_join;
            }
        }

        file_join_final = string.Join("\n", file_join_general);
           * */

          /*
           * ВЫВОД МАССИВА, ЗАПИСАННОГО В ФАЙЛ *
            Console.WriteLine("File: {0} ", file_join_final);
           */

          using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
          {
              sw.WriteLine(file_join_final);
          }
                  


             
            
            Console.ReadKey();

        }
    }
}
