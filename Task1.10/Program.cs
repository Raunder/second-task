using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_00
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAttack = "1";
            const string CommandFireball = "2";
            const string CommandExplosion = "3";
            const string CommandUseItem = "4";
            const string CommandSurrender = "8";
            const string CommandExit = "9";

            string enemy = "Босс";
            string item = "\"Зелье Восстановления\"";
            string attack = "Обычная атака";
            string fireball = "Огненный шар";
            string explosion = "Взрыв";
            string useItem = "Использовать";
            string surrender = "Сдаться";

            int healthDead = 0;
            int healthBoss = 100;
            int healthHero = 100;
            int maxHealthHero = 100;
            int manaHero = 50;
            int maxManaHero = 50;
            int minBossDamage = 10;
            int maxBossDamage = 40;

            int quantityBottles = 2;

            int damageHeroDefault = 10;
            int damageHeroFireboll = 25;
            int manaDamageHeroFireboll = 15;
            int damageHeroExplosion = 45;
            int manaDamageHeroExplosion = 25;

            int damageHeroSuicide = 999;

            List<string> commands = new List<string>()
            { $"{CommandAttack} - {attack} ({damageHeroDefault} ед. урона)",
                $"{CommandFireball} - {fireball} ({damageHeroFireboll} ед. урона {manaDamageHeroFireboll} ед. маны)",
                $"{CommandExplosion} - {explosion} ({damageHeroExplosion} ед. урона {manaDamageHeroExplosion} ед. маны /// можно использовать только после {fireball})",
                $"{CommandUseItem} - {useItem} {item} (восстанавливает здоровье и ману на максимум)",
                $"{CommandSurrender} - {surrender}",
                $"{CommandExit} - Выход"};
            var info = string.Join("\n", commands);

            bool isWork = true;
            bool checkMoveBoss = false;
            bool isItUsedFireball = false;
            string userInput;

            while (isWork)
            {
                Random random = new Random();
                int damageBoss = random.Next(minBossDamage, maxBossDamage);

                healthHero -= damageBoss;
                Console.WriteLine($"Вас атаковал {enemy}! Вам было нанесено {damageBoss} единиц урона!\n\nЗдоровье Противника:{healthBoss}\n\nЗдоровье: {healthHero}\nМана:{manaHero}\n\n");

                if (healthBoss > healthDead & healthHero > healthDead)
                {
                    checkMoveBoss = true;
                }
                else if (healthBoss <= healthDead)
                {
                    isWork = false;
                    Console.WriteLine("Вы победили босса! Ура!!!\n");
                }
                else
                {
                    isWork = false;
                    Console.WriteLine($"Вы погибли в жесткой схватке с противником \"{enemy}\"... \nВы проиграли.\n");
                }

                while (checkMoveBoss)
                {
                    Console.WriteLine($"\n{info}\n");
                    Console.WriteLine($"В наличии {item}: {quantityBottles}");
                    Console.WriteLine("Ваше действие: ");
                    userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case CommandAttack:
                            healthBoss -= damageHeroDefault;
                            checkMoveBoss = false;
                            Console.Clear();
                            isItUsedFireball = false;
                            break;

                        case CommandFireball:
                            if (manaHero >= manaDamageHeroFireboll)
                            {
                                healthBoss -= damageHeroFireboll;
                                manaHero -= manaDamageHeroFireboll;
                            }
                            else
                            {
                                Console.WriteLine($"У вас недостаточно маны!\n");
                            }
                            isItUsedFireball = true;
                            checkMoveBoss = false;
                            Console.Clear();
                            break;

                        case CommandExplosion:
                            Console.Clear();

                            if (isItUsedFireball & manaHero >= manaDamageHeroExplosion)
                            {
                                healthBoss -= damageHeroExplosion;
                                manaHero -= manaDamageHeroExplosion;
                            }
                            else if (manaHero < manaDamageHeroExplosion)
                            {
                                Console.WriteLine($"У вас недостаточно маны!\n");
                            }
                            else
                            {
                                Console.WriteLine($"Вы не использовали {fireball}\n");
                            }

                            isItUsedFireball = false;
                            checkMoveBoss = false;
                            break;

                        case CommandUseItem:
                            Console.Clear();

                            if (quantityBottles > 1)
                            {
                                healthHero = maxHealthHero;
                                manaHero = maxManaHero;
                                quantityBottles -= 1;
                                Console.WriteLine($"Вы использовали {item}. Ваши здоровье и мана восстановлены! \nУ вас осталось {quantityBottles}.\n");
                            }
                            else
                            {
                                Console.WriteLine($"У вас не осталось {item}\n");
                            }

                            checkMoveBoss = false;
                            isItUsedFireball = false;
                            break;

                        case CommandSurrender:
                            Console.Clear();
                            Console.WriteLine($"Вы решили сдаться...\nЧто ж, это тоже выбор и его нужно уважать.\nВам было нанесено {damageHeroSuicide} ед. урона\n");
                            healthHero -= damageHeroSuicide;
                            checkMoveBoss = false;
                            break;

                        case CommandExit:
                            checkMoveBoss = false;
                            isWork = false;
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("Вы ничего не использовали и пропускаете ход!\n");
                            checkMoveBoss = false;
                            isItUsedFireball = false;
                            break;
                    }
                }
            }

            Console.ReadKey();

        }
    }
}