using GraphQLParser.AST;

namespace GQLCCG.Processor.SchemaReaders
{
    public interface ISchemaParser
    {
        GraphQLDocument Parse(string schema);
    }
}