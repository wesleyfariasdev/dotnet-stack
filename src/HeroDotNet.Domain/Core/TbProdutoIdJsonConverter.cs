using System.Text.Json.Serialization;
using System.Text.Json;

namespace HeroDotNet.Domain.Core;

public class TbProdutoIdJsonConverter : JsonConverter<TbProdutoId>
{
    public override TbProdutoId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => TbProdutoId.FromGuid(Guid.Parse(reader.GetString()!));

    public override void Write(Utf8JsonWriter writer, TbProdutoId value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString());
}
