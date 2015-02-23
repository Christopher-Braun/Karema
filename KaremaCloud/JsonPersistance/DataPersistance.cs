
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using JsonPersistance;
using KaReMa.Interfaces;
using Mvc4WebRole.Models;

namespace Mvc4WebRole
{
    public class DataPersistance
    {
        public static Stream SaveData(IEnumerable<RecipeModel> recipes, IEnumerable<TagModel> tags)
        {
            DataCollection dataCollection = new DataCollection();

            var tagData = tags.Select(t => t.ToData()).ToList();
            tagData.ForEach(x => dataCollection.Tags.Add(x));

            var recipeData = recipes.Select(t => t.ToData()).ToList();
            recipeData.ForEach(x => dataCollection.Recipes.Add(x));

            var serializer = new DataContractJsonSerializer(typeof (DataCollection));

            Stream stream = new MemoryStream();

            serializer.WriteObject(stream, dataCollection);

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}