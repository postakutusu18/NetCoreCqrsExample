using Core.ElasticSearch.Models;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using Nest.JsonNetSerializer;
using Newtonsoft.Json;

namespace Core.ElasticSearch;



public class ElasticSearchManager : IElasticSearch
{
    private readonly ConnectionSettings _connectionSettings;

    public ElasticSearchManager(ElasticSearchConfig configuration)
    {
        SingleNodeConnectionPool connectionPool = new SingleNodeConnectionPool(new Uri(configuration.ConnectionString));
        _connectionSettings = new ConnectionSettings(connectionPool, (IElasticsearchSerializer builtInSerializer, IConnectionSettingsValues connectionSettings) => new JsonNetSerializer(builtInSerializer, connectionSettings, () => new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));
    }

    public IReadOnlyDictionary<IndexName, IndexState> GetIndexList()
    {
        return new ElasticClient(_connectionSettings).Indices.Get(new GetIndexRequest(Indices.All)).Indices;
    }

    public async Task<IElasticSearchResult> InsertManyAsync(string indexName, object[] items)
    {
        string indexName2 = indexName;
        object[] items2 = items;
        await getElasticClient(indexName2).BulkAsync((BulkDescriptor a) => a.Index(indexName2).IndexMany(items2));
        return new ElasticSearchResult();
    }

    public async Task<IElasticSearchResult> CreateNewIndexAsync(IndexModel indexModel)
    {
        IndexModel indexModel2 = indexModel;
        ElasticClient elasticClient = getElasticClient(indexModel2.IndexName);
        if (elasticClient.Indices.Exists(indexModel2.IndexName).Exists)
        {
            return new ElasticSearchResult(success: false, "Index already exists");
        }

        CreateIndexResponse createIndexResponse = await elasticClient.Indices.CreateAsync(indexModel2.IndexName, (CreateIndexDescriptor se) => se.Settings((IndexSettingsDescriptor a) => a.NumberOfReplicas(indexModel2.NumberOfReplicas).NumberOfShards(indexModel2.NumberOfShards)).Aliases((AliasesDescriptor x) => x.Alias(indexModel2.AliasName)));
        return new ElasticSearchResult(createIndexResponse.IsValid, createIndexResponse.IsValid ? "Success" : createIndexResponse.ServerError.Error.Reason);
    }

    public async Task<IElasticSearchResult> DeleteByElasticIdAsync(ElasticSearchModel model)
    {
        ElasticSearchModel model2 = model;
        DeleteResponse deleteResponse = await getElasticClient(model2.IndexName).DeleteAsync(model2.ElasticId, (DeleteDescriptor<object> x) => x.Index(model2.IndexName));
        return new ElasticSearchResult(deleteResponse.IsValid, deleteResponse.IsValid ? "Success" : deleteResponse.ServerError.Error.Reason);
    }

    public async Task<List<ElasticSearchGetModel<T>>> GetAllSearch<T>(SearchParameters parameters) where T : class
    {
        SearchParameters parameters2 = parameters;
        return (await getElasticClient(parameters2.IndexName).SearchAsync((SearchDescriptor<T> s) => s.Index(Indices.Index(parameters2.IndexName)).From(parameters2.From).Size(parameters2.Size))).Hits.Select((IHit<T> x) => new ElasticSearchGetModel<T>
        {
            ElasticId = x.Id,
            Item = x.Source
        }).ToList();
    }

    public async Task<List<ElasticSearchGetModel<T>>> GetSearchByField<T>(SearchByFieldParameters fieldParameters) where T : class
    {
        SearchByFieldParameters fieldParameters2 = fieldParameters;
        return (await getElasticClient(fieldParameters2.IndexName).SearchAsync((SearchDescriptor<T> s) => s.Index(fieldParameters2.IndexName).From(fieldParameters2.From).Size(fieldParameters2.Size))).Hits.Select((IHit<T> x) => new ElasticSearchGetModel<T>
        {
            ElasticId = x.Id,
            Item = x.Source
        }).ToList();
    }

    public async Task<List<ElasticSearchGetModel<T>>> GetSearchBySimpleQueryString<T>(SearchByQueryParameters queryParameters) where T : class
    {
        SearchByQueryParameters queryParameters2 = queryParameters;
        return (await getElasticClient(queryParameters2.IndexName).SearchAsync((SearchDescriptor<T> s) => s.Index(queryParameters2.IndexName).From(queryParameters2.From).Size(queryParameters2.Size)
            .MatchAll()
            .Query((QueryContainerDescriptor<T> a) => a.SimpleQueryString((SimpleQueryStringQueryDescriptor<T> c) => c.Name(queryParameters2.QueryName).Boost(1.1).Fields(queryParameters2.Fields)
                .Query(queryParameters2.Query)
                .Analyzer("standard")
                .DefaultOperator(Operator.Or)
                .Flags(SimpleQueryStringFlags.And | SimpleQueryStringFlags.Near)
                .Lenient(true)
                .AnalyzeWildcard(false)
                .MinimumShouldMatch("30%")
                .FuzzyPrefixLength(0)
                .FuzzyMaxExpansions(50)
                .FuzzyTranspositions(true)
                .AutoGenerateSynonymsPhraseQuery(false))))).Hits.Select((IHit<T> x) => new ElasticSearchGetModel<T>
                {
                    ElasticId = x.Id,
                    Item = x.Source
                }).ToList();
    }

    public async Task<IElasticSearchResult> InsertAsync(ElasticSearchInsertUpdateModel model)
    {
        ElasticSearchInsertUpdateModel model2 = model;
        IndexResponse ındexResponse = await getElasticClient(model2.IndexName).IndexAsync(model2.Item, (IndexDescriptor<object> i) => i.Index(model2.IndexName).Id(model2.ElasticId).Refresh(Refresh.True));
        return new ElasticSearchResult(ındexResponse.IsValid, ındexResponse.IsValid ? "Success" : ındexResponse.ServerError.Error.Reason);
    }

    public async Task<IElasticSearchResult> UpdateByElasticIdAsync(ElasticSearchInsertUpdateModel model)
    {
        ElasticSearchInsertUpdateModel model2 = model;
        UpdateResponse<object> updateResponse = await getElasticClient(model2.IndexName).UpdateAsync<object>((DocumentPath<object>)model2.ElasticId, (Func<UpdateDescriptor<object, object>, IUpdateRequest<object, object>>)((UpdateDescriptor<object, object> u) => u.Index(model2.IndexName).Doc(model2.Item)), default(CancellationToken));
        return new ElasticSearchResult(updateResponse.IsValid, updateResponse.IsValid ? "Success" : updateResponse.ServerError.Error.Reason);
    }

    private ElasticClient getElasticClient(string indexName)
    {
        if (string.IsNullOrEmpty(indexName))
        {
            throw new ArgumentNullException(indexName, "Index name cannot be null or empty ");
        }

        return new ElasticClient(_connectionSettings);
    }
}