using System;
using System.Collections.Generic;
using Poke.Core.Commands.Requests;
using Poke.Core.DTOs;
using Poke.Core.Entities;
using Poke.Core.Models;

namespace Poke.Core.ValueObjects
{
    public class BaseStats : ValueObject
    {
        public Point HitPoints { get; protected set; }
        public Point Attack { get; protected set; }
        public Point Defense { get; protected set; }
        public Point SpecialAttack { get; protected set; }
        public Point SpecialDefense { get; protected set; }
        public Point Speed { get; protected set; }
        public int TotalPoints {
            get => HitPoints.Value + Attack.Value + Defense.Value +
                SpecialAttack.Value + SpecialDefense.Value + Speed.Value;
        }
        public int PokemonNumber { get; protected set; }
        public Pokemon Pokemon { get; protected set; }

        public BaseStats()
        {}

        public BaseStats(BaseStatsRequest request)
        {
            HitPoints = new Point(request.HitPoints);
            Attack = new Point(request.Attack);
            Defense = new Point(request.Defense);
            SpecialAttack = new Point(request.SpecialAttack);
            SpecialDefense = new Point(request.SpecialDefense);
            Speed = new Point(request.Speed);
        }

        public BaseStats(
            Point hitPoints, Point attack, Point defense,
            Point specialAttack, Point specialDefense, Point speed,
            int pokemonNumber
        )
        {
            HitPoints = hitPoints;
            Attack = attack;
            Defense = defense;
            SpecialAttack = specialAttack;
            SpecialDefense = specialDefense;
            Speed = speed;
            PokemonNumber = pokemonNumber;
        }

        private BaseStats(BaseStatsDTO dto) : base(dto.Id)
        {
            HitPoints = new Point(dto.HitPoints);
            Attack = new Point(dto.Attack);
            Defense = new Point(dto.Defense);
            SpecialAttack = new Point(dto.SpecialAttack);
            SpecialDefense = new Point(dto.SpecialDefense);
            Speed = new Point(dto.Speed);
        }

        public static BaseStats FromBaseStatsDTO(BaseStatsDTO dto)
        {
            return new BaseStats(dto);
        }

        public void UpdatePokemonBaseStatsData(BaseStatsRequest request)
        {
            HitPoints = new Point(request.HitPoints);
            Attack = new Point(request.Attack);
            Defense = new Point(request.Defense);
            SpecialAttack = new Point(request.SpecialAttack);
            SpecialDefense = new Point(request.SpecialDefense);
            Speed = new Point(request.Speed);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return HitPoints.Value;
            yield return Attack.Value;
            yield return Defense.Value;
            yield return SpecialAttack.Value;
            yield return SpecialDefense.Value;
            yield return Speed.Value;
        }
    }
}
