using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CP5_.NET.Models;

public class RelatorioEstoque
{
    [BsonElement("plataforma")]
    public string Plataforma { get; set; } = string.Empty;

    [BsonElement("quantidadeTitulos")]
    public int QuantidadeTitulos { get; set; }

    [BsonElement("quantidadeUnidades")]
    public long QuantidadeUnidades { get; set; }

    [BsonElement("valorTotalEstoque")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal ValorTotalEstoque { get; set; }
}