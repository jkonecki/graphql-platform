using System;
using System.Text.RegularExpressions;
using HotChocolate.Data.Filters;
using HotChocolate.Language;
using HotChocolate.Types;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HotChocolate.Data.MongoDb.Filters;

public class MongoDbStringNotEndsWithHandler
    : MongoDbStringOperationHandler
{
    public MongoDbStringNotEndsWithHandler(InputParser inputParser)
        : base(inputParser)
    {
        CanBeNull = false;
    }

    protected override int Operation => DefaultFilterOperations.NotEndsWith;

    public override MongoDbFilterDefinition HandleOperation(
        MongoDbFilterVisitorContext context,
        IFilterOperationField field,
        IValueNode value,
        object? parsedValue)
    {
        if (parsedValue is string str)
        {
            var doc = new NotMongoDbFilterDefinition(
                new MongoDbFilterOperation(
                    "$regex",
                    new BsonRegularExpression($"/{Regex.Escape(str)}$/")));

            return new MongoDbFilterOperation(context.GetMongoFilterScope().GetPath(), doc);
        }

        throw new InvalidOperationException();
    }
}
