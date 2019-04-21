using GraphQLParser;
using GraphQLParser.AST;

namespace GQLCCG.Processor.SchemaReaders
{
    public class LexerGraphQlSchemaParser : ISchemaParser
    {
        public GraphQLDocument Parse(string schema)
        {
            var lexer = new Lexer();
            var parser = new Parser(lexer);
            var result = parser.Parse(new Source(schema));

            return result;
        }
    }
}