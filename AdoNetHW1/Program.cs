using System.Data.SqlClient;
using System.Text;

namespace AdoNetHW1
{
    internal class Program
    {
        public static string ConnectionString => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VegAndFruit;Integrated Security=True;Connect Timeout=30;";
        static void Main()
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            int choice;
            do
            {

                Console.Clear();
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Відобразити всю інформацію з таблиці овочів і фруктів.");
                Console.WriteLine("2. Відобразити усі назви овочів і фруктів.");
                Console.WriteLine("3. Відобразити усі кольори.");
                Console.WriteLine("4. Показати максимальну калорійність.");
                Console.WriteLine("5. Показати мінімальну калорійність.");
                Console.WriteLine("6. Показати середню калорійність.");
                Console.WriteLine("7. Показати кількість овочів.");
                Console.WriteLine("8. Показати кількість фруктів.");
                Console.WriteLine("9. Показати кількість овочів і фруктів заданого кольору.");
                Console.WriteLine("10. Показати кількість овочів і фруктів кожного кольору.");
                Console.WriteLine("11. Показати овочі та фрукти з калорійністю нижче 25.");
                Console.WriteLine("12. Показати овочі та фрукти з калорійністю вище 25.");
                Console.WriteLine("13. Показати овочі та фрукти з калорійністю у діапазоні з 10 до 45.");
                Console.WriteLine("14. Показати усі овочі та фрукти жовтого або червоного кольору.");
                Console.WriteLine("0. Вийти з програми");

                Console.Write("Виберіть опцію: ");
                choice = int.Parse(Console.ReadLine());
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        connection.Open();


                        switch (choice)
                        {
                            case 1:
                                DisplayAllVegetablesAndFruits(connection);
                                break;
                            case 2:
                                DisplayAllNames(connection);
                                break;
                            case 3:
                                DisplayAllColors(connection);
                                break;
                            case 4:
                                DisplayMaxCalories(connection);
                                break;
                            case 5:
                                DisplayMinCalories(connection);
                                break;
                            case 6:
                                DisplayAverageCalories(connection);
                                break;
                            case 7:
                                DisplayVegetableCount(connection);
                                break;
                            case 8:
                                DisplayFruitCount(connection);
                                break;
                            case 9:
                                DisplayVegetablesAndFruitsByColor(connection);
                                break;
                            case 10:
                                DisplayVegetablesAndFruitsCountByColor(connection);
                                break;
                            case 11:
                                DisplayVegetablesAndFruitsByCalories(connection, "<");
                                break;
                            case 12:
                                DisplayVegetablesAndFruitsByCalories(connection , ">");
                                break;
                            case 13:
                                DisplayVegetablesAndFruitsByCaloriesRange(connection);
                                break;
                            case 14:
                                DisplayVegetablesAndFruitsByColorCategory(connection);
                                break;
                            case 0:
                                Console.WriteLine("Poka!");
                                break;
                            default:
                                Console.WriteLine("Неправильний вибір.");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка: {ex.Message}");
                    }
                }
                Thread.Sleep(5000);
            } while (choice != 0);

            static void DisplayAllVegetablesAndFruits(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM VegetablesAndFruits", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Назва: {reader["Name"]}, Тип: {reader["Type"]}, Колір: {reader["Color"]}, Калорійність: {reader["Calories"]}");
                    }
                }
            }

            static void DisplayAllNames(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT Name FROM VegetablesAndFruits", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Назва: {reader["Name"]}");
                    }
                }
            }

            static void DisplayAllColors(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT DISTINCT Color FROM VegetablesAndFruits", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Колір: {reader["Color"]}");
                    }
                }
            }

            static void DisplayMaxCalories(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT TOP 1 Name, MAX(Calories) AS MaxCalories FROM VegetablesAndFruits GROUP BY Name ORDER BY MaxCalories DESC", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Максимальна калорійність:Назва {reader["Name"]}  {reader["MaxCalories"]}");
                    }
                }
            }

            static void DisplayMinCalories(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT TOP 1 Name, MIN(Calories) AS MinCalories FROM VegetablesAndFruits GROUP BY Name ORDER BY MinCalories", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Мінімальна калорійність:Назва {reader["Name"]}  {reader["MinCalories"]}");
                    }
                }
            }

            static void DisplayAverageCalories(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT AVG(Calories) AS AvgCalories FROM VegetablesAndFruits", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Середня калорійність:{reader["AvgCalories"]}");
                    }
                }
            }

            static void DisplayVegetableCount(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) AS VegetableCount FROM VegetablesAndFruits WHERE IsFruit = 'False'", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Кількість овочів: {reader["VegetableCount"]}");
                    }
                }
            }

            static void DisplayFruitCount(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) AS FruitCount FROM VegetablesAndFruits WHERE IsFruit = 'True'", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Кількість фруктів: {reader["FruitCount"]}");
                    }
                }
            }

            static void DisplayVegetablesAndFruitsByColor(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) AS CountByColor FROM VegetablesAndFruits WHERE Color = 'green'", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Кількість овочів та фруктів кольору зеленого: {reader["CountByColor"]}");
                    }
                }
            }

            static void DisplayVegetablesAndFruitsCountByColor(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT Color, COUNT(*) AS CountByColor FROM VegetablesAndFruits GROUP BY Color", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Кількість овочів та фруктів кольору {reader["Color"]}: {reader["CountByColor"]}");
                    }
                }
            }

            static void DisplayVegetablesAndFruitsByCalories(SqlConnection connection, string comparisonOperator)
            {
                using (SqlCommand command = new SqlCommand($"SELECT * FROM VegetablesAndFruits WHERE Calories {comparisonOperator} 25", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Назва: {reader["Name"]} Калорійність: {reader["Calories"]}");
                    }
                }
            }

            static void DisplayVegetablesAndFruitsByColorCategory(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM VegetablesAndFruits WHERE Color IN ('red', 'yellow')", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Назва: {reader["Name"]} Колір: {reader["Color"]}");
                    }
                }
            }

            static void DisplayVegetablesAndFruitsByCaloriesRange(SqlConnection connection)
            {
                using (SqlCommand command = new SqlCommand("SELECT Name, Calories FROM VegetablesAndFruits WHERE Calories BETWEEN 10 AND 45", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Назва: {reader["Name"]} Калорії: {reader["Calories"]}");
                    }
                }
            }


        }
    }
}