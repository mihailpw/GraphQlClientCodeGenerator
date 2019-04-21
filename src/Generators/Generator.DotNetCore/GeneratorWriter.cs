using System.Collections.Generic;
using System.Threading.Tasks;
using GQLCCG.Infra;
using GraphQLParser.AST;

namespace Generator.DotNetCore
{
    internal class GeneratorWriter
    {
        private readonly IGeneratorWriter _writer;


        public GeneratorWriter(IGeneratorWriter writer)
        {
            _writer = writer;
        }


        public async Task GenerateAsync(IEnumerable<ASTNode> nodes)
        {
            foreach (var definition in nodes)
            {
                switch (definition)
                {
                    case GraphQLArgument argument:
                        await _writer.WriteAsync(argument.Name.Value);
                        break;
                    case GraphQLDirective directive:
                        break;
                    case GraphQLDirectiveDefinition directiveDefinition:
                        break;
                    case GraphQLDocument document:
                        await GenerateAsync(document.Definitions);
                        break;
                    case GraphQLEnumTypeDefinition enumTypeDefinition:
                        break;
                    case GraphQLEnumValueDefinition enumValueDefinition:
                        break;
                    case GraphQLFieldDefinition fieldDefinition:
                        await _writer.WriteAsync(fieldDefinition.Name.Value);
                        break;
                    case GraphQLFieldSelection fieldSelection:
                        await _writer.WriteAsync(fieldSelection.Name.Value);
                        break;
                    case GraphQLFragmentDefinition fragmentDefinition:
                        break;
                    case GraphQLFragmentSpread fragmentSpread:
                        break;
                    case GraphQLInlineFragment inlineFragment:
                        break;
                    case GraphQLInputObjectTypeDefinition inputObjectTypeDefinition:
                        break;
                    case GraphQLInputValueDefinition inputValueDefinition:
                        break;
                    case GraphQLInterfaceTypeDefinition interfaceTypeDefinition:
                        break;
                    case GraphQLListType listType:
                        break;
                    case GraphQLListValue listValue:
                        break;
                    case GraphQLName name:
                        await _writer.WriteAsync(name.Value);
                        break;
                    case GraphQLNamedType namedType:
                        break;
                    case GraphQLNonNullType nonNullType:
                        break;
                    case GraphQLObjectField objectField:
                        break;
                    case GraphQLObjectTypeDefinition objectTypeDefinition:
                        await GenerateAsync(objectTypeDefinition.Fields);
                        break;
                    case GraphQLObjectValue objectValue:
                        break;
                    case GraphQLOperationDefinition operationDefinition:
                        break;
                    case GraphQLOperationTypeDefinition operationTypeDefinition:
                        break;
                    case GraphQLScalarTypeDefinition scalarTypeDefinition:
                        break;
                    case GraphQLScalarValue scalarValue:
                        break;
                    case GraphQLSchemaDefinition schemaDefinition:
                        break;
                    case GraphQLSelectionSet selectionSet:
                        break;
                    case GraphQLType type:
                        break;
                    case GraphQLTypeExtensionDefinition typeExtensionDefinition:
                        break;
                    case GraphQLUnionTypeDefinition unionTypeDefinition:
                        break;
                    case GraphQLTypeDefinition typeDefinition:
                        break;
                    case GraphQLVariable variable:
                        break;
                    case GraphQLValue value:
                        break;
                    case GraphQLVariableDefinition variableDefinition:
                        break;
                }
            }
        }
    }
}