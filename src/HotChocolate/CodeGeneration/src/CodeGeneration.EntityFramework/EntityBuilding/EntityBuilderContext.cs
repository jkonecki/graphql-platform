using System;
using HotChocolate.CodeGeneration.EntityFramework.Types;
using HotChocolate.Types;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HotChocolate.CodeGeneration.EntityFramework.ModelBuilding
{
    public class EntityBuilderContext
    {
        public SchemaConventionsDirective Conventions { get; }

        public string Namespace { get; }

        public ObjectType ObjectType { get; }

        public string? EntityName { get; set; }

        public string RequiredEntityName
            => EntityName ?? throw new Exception("Entity name is required");

        public string? EntityNamePluralized { get; set; }

        public string RequiredEntityNamePluralized
            => EntityNamePluralized ?? throw new Exception("Entity name pluralized is required");

        public string? EntityConfigurerName => $"{EntityName}Configurer";

        public string RequiredEntityConfigurerName
            => EntityConfigurerName ?? throw new Exception("Entity configurer name is required");

        public bool IsBackedByTable { get; set; }

        public string? TableName { get; set; }

        public TableDirective? TableDirective { get; set; }

        public PrimaryKeyField[]? PrimaryKey { get; set; }

        public ClassDeclarationSyntax? EntityClass { get; set; }

        public ClassDeclarationSyntax? EntityConfigurerClass { get; set; }

        public UsingDirectiveSyntax[] EntityConfigurerUsings { get; } = SyntaxConstants.ModelConfigurerUsings;

        public EntityBuilderContext(SchemaConventionsDirective conventions, string @namespace, ObjectType objectType)
        {
            Conventions = conventions;
            Namespace = @namespace ?? throw new ArgumentNullException(nameof(@namespace));
            ObjectType = objectType ?? throw new ArgumentNullException(nameof(objectType));
        }
    }
}