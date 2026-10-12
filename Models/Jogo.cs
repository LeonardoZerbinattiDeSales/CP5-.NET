using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CP5_.NET.Models;

public class Jogo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório.")]
    [BsonElement("titulo")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "A plataforma é obrigatória.")]
    [BsonElement("plataforma")]
    public string Plataforma { get; set; } = string.Empty;

    [Required(ErrorMessage = "O gênero é obrigatório.")]
    [BsonElement("genero")]
    public string Genero { get; set; } = string.Empty;

    [BsonElement("preco")]
    [BsonRepresentation(BsonType.Decimal128)]
    [Range(typeof(decimal), "0.01", "999999.99",
        ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Preco { get; set; }

    [Range(1950, 2100,
        ErrorMessage = "O ano de lançamento informado é inválido.")]
    [BsonElement("anoLancamento")]
    public int AnoLancamento { get; set; }

    [Range(0, int.MaxValue,
        ErrorMessage = "O estoque não pode ser negativo.")]
    [BsonElement("estoque")]
    public int Estoque { get; set; }
}