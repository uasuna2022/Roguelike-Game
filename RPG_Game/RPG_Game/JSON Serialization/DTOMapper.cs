using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Game.EnumClasses;
using RPG_Game.Interfaces;
using RPG_Game.Items.Currency;
using RPG_Game.Items.UnusableItems;
using RPG_Game.MVC_Pattern.Model;
using RPG_Game.PotionEffects;

namespace RPG_Game.JSON_Serialization
{
    public static class DTOMapper
    {
        public static ItemDTO ConvertToDTO(IItem item) // potem dodać poprawną obsługę dekoratorów
        {
            if (item == null) return null!;

            Type type = item.GetType();

            ItemDTO itemDTO = new ItemDTO
            {
                Name = item.Name,
                Symbol = item.Symbol,
                ConsoleColor = item.ConsoleColor,
                IsDrinkable = item.IsDrinkable,
                IsEquippable = item.IsEquippable,
                Type = type.Name,
                Properties = new Dictionary<string, object>()
            };

            if (typeof(IWeapon).IsAssignableFrom(type))
            {
                IWeapon weapon = (IWeapon)item;
                itemDTO.Properties["damage"] = weapon.Damage;
                itemDTO.Properties["twoHanded"] = weapon.IsTwoHanded;
            }

            if (typeof(IPotion).IsAssignableFrom(type))
            {
                itemDTO.Properties["potionType"] = type.Name;
            }

            if (type == typeof(Coin))
            {
                Coin coin = (Coin)item;
                itemDTO.Properties["amount"] = coin.Amount;
            }
            else if (type == typeof(Gold))
            {
                Gold gold = (Gold)item;
                itemDTO.Properties["amount"] = gold.Amount;
            }
            else if (type == typeof(Book))
            {
                Book book = (Book)item;
                itemDTO.Properties["description"] = book.Description;
            }
            else if (type == typeof(Ring))
            {
                Ring ring = (Ring)item;
                itemDTO.Properties["description"] = ring.Description;
            }

            return itemDTO;
        }

        public static WeaponDTO ConvertToDTO(IWeapon weapon)
        {
            if (weapon == null) return null!;

            return new WeaponDTO
            {
                Name = weapon.Name,
                Symbol = weapon.Symbol,
                ConsoleColor = weapon.ConsoleColor,
                IsDrinkable = weapon.IsDrinkable,
                IsEquippable = weapon.IsEquippable,
                Damage = weapon.Damage,
                IsTwoHanded = weapon.IsTwoHanded,
                Type = weapon.GetType().Name,
                Properties = new Dictionary<string, object>()
            };
        } 
        // być może będzie niepotrzebne, skoro i tak mam obsługę IWeaponów wewnątrz funkcji ItemDTO ConvertToDTO    
        public static PotionEffectDTO ConvertToDTO(PotionEffectBaseClass effect)
        {
            if (effect == null) return null!;

            Type type = effect.GetType();

            PotionEffectDTO potionEffectDTO = new PotionEffectDTO
            {
                Type = type.Name,
                Properties = new Dictionary<string, object>()
            };

            if (typeof(JuiceInfiniteEffect).IsAssignableFrom(type))
            {
                JuiceInfiniteEffect j = (JuiceInfiniteEffect)effect;
                potionEffectDTO.Properties["dexterityBoost"] = j.DexterityBoost;
                potionEffectDTO.Properties["duration"] = "infinite";
            }
            else if (typeof(AntiPotionEffect).IsAssignableFrom(type))
            {
                AntiPotionEffect a = (AntiPotionEffect)effect;
                potionEffectDTO.Properties["antidote"] = true;
            }
            else if (typeof(PowerPotionEffect).IsAssignableFrom(type))
            {
                PowerPotionEffect p = (PowerPotionEffect)effect;
                potionEffectDTO.Properties["strengthBoost"] = p.StrengthBoost;
            }    
            else if (typeof(LuckPotionEffect).IsAssignableFrom(type))
            {
                LuckPotionEffect l = (LuckPotionEffect)effect;
                potionEffectDTO.Properties["originalLuck"] = l.OriginalLuck;
                potionEffectDTO.Properties["totalTurns"] = l.TotalTurns;
            }

            // TODO: add turnsRemaining property

            return potionEffectDTO;
        } // *

        public static CellDTO ConvertToDTO(Cell cell)
        {
            if (cell == null) return null!;

            CellDTO cellDTO = new CellDTO()
            {
                X = cell.X,
                Y = cell.Y,
                IsWall = cell.isWall,
                ContainsPlayer = cell.ContainsPlayer,
                Items = new List<ItemDTO>(),
                Enemy = null
            };

            if (cell.Enemy != null)
                cellDTO.Enemy = ConvertToDTO(cell.Enemy);

            if (cell.Items != null && cell.Items.Count > 0)
            {
                foreach (var item in cell.Items)
                {
                    cellDTO.Items.Add(ConvertToDTO(item));
                }
            }

            return cellDTO;
        }

        public static RoomDTO ConvertToDTO(Room room)
        {
            if (room == null) return null!;

            RoomDTO roomDTO = new RoomDTO
            {
                Width = room.Width,
                Height = room.Height,
                GridCells = new List<CellDTO>(room.Width * room.Height)
            };

            for (int i = 0; i < room.Height; i++)
            {
                for (int j = 0; j < room.Width; j++)
                {
                    Cell cell = room.GetCell(i, j);
                    roomDTO.GridCells.Add(ConvertToDTO(cell)); 
                }
            }

            return roomDTO;
        }

        public static EnemyDTO ConvertToDTO(IEnemy enemy)
        {
            if (enemy == null) return null!;

            return new EnemyDTO
            {
                Name = enemy.Name,
                Color = enemy.Color,
                Symbol = enemy.Symbol,
                Armor = enemy.Armor,
                Health = enemy.Health,
                Damage = enemy.Damage
            };
        }
    
        public static PlayerDTO ConvertToDTO(Player player)
        {
            if (player == null) return null!; 
            
            PlayerDTO playerDTO = new PlayerDTO
            {
                X = player.X,
                Y = player.Y,

                Strength = player.Strength,
                Dexterity = player.Dexterity,
                Luck = player.Luck,
                Wisdom = player.Wisdom,
                Aggression = player.Aggression,
                Health = player.Health,

                Gold = player.Gold,
                Coins = player.Coins,

                LeftHand = (player.LeftHand == null) ? null : ConvertToDTO(player.LeftHand),
                RightHand = (player.RightHand == null) ? null : ConvertToDTO(player.RightHand),

                Inventory = new List<ItemDTO>(),
                NearbyEnemies = new Dictionary<string, EnemyDTO?>(),
                ActivePlayerEffects = new List<PotionEffectDTO>()
            };

            if (player.Inventory != null && player.Inventory.Count > 0)
            {
                foreach (var item in player.Inventory)
                    playerDTO.Inventory.Add(ConvertToDTO(item));
            }

            if (player.activeEffects != null && player.activeEffects.Count > 0)
            {
                foreach (var effect in player.activeEffects)
                    playerDTO.ActivePlayerEffects.Add(ConvertToDTO(effect));
            }

            playerDTO.NearbyEnemies["left"] = (player.nearbyEnemies[Direction.Left] == null)
                ? null : ConvertToDTO(player.nearbyEnemies[Direction.Left]!);
            playerDTO.NearbyEnemies["right"] = (player.nearbyEnemies[Direction.Right] == null)
                ? null : ConvertToDTO(player.nearbyEnemies[Direction.Right]!);
            playerDTO.NearbyEnemies["up"] = (player.nearbyEnemies[Direction.Up] == null)
                ? null : ConvertToDTO(player.nearbyEnemies[Direction.Up]!);
            playerDTO.NearbyEnemies["down"] = (player.nearbyEnemies[Direction.Down] == null)
                ? null : ConvertToDTO(player.nearbyEnemies[Direction.Down]!);

            return playerDTO;
        }

        public static GameStateDTO ConvertToDTO(GameState gameState)
        {
            if (gameState == null) return null!;

            GameStateDTO gameStateDTO = new GameStateDTO
            {
                Version = gameState.Version,
                StepCounter = gameState.StepCounter,
                Room = ConvertToDTO(gameState.Room),
                Players = new List<PlayerDTO>(),
            };

            if (gameState.Players != null && gameState.Players.Count > 0)
            {
                foreach (var player in gameState.Players)
                {
                    gameStateDTO.Players.Add(ConvertToDTO(player));
                }
            }

            return gameStateDTO;
        }
    }
}
