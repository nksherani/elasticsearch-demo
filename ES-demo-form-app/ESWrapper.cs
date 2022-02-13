using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES_demo_form_app
{
    public class ESWrapper
    {
        public static Uri node;
        public static ConnectionSettings settings;
        public static ElasticClient client;
        public ESWrapper(string defaultIndex)
        {
            node = new Uri("http://localhost:9200");
            settings = new ConnectionSettings(node);
            settings.DefaultIndex(defaultIndex);
            client = new ElasticClient(settings);
           
        }
        public CreateIndexResponse CreateIndex(string index)
        {
            var response = client.Indices.Create(index,
                     index => index.Map<Student>(
                         x => x.AutoMap()
                     ));
            return response;
        }
        public DeleteIndexResponse DeleteIndex(string index)
        {
            var response = client.Indices.Delete(index);
            return response;
        }
        public ISearchResponse<Student> GetStudentsById(string id)
        {
            var students = client.Search<Student>(s => s.Query(q => q.Match(m => m.Field(f => f.StudentId).Query(id))));
            return students;
        }
        public IEnumerable<Student> SearchMatchQuery(string name)
        {
            var students = client.Search<Student>(s => s.Query(q => q.Match(m => m.Field(f => f.Content).Query(name))));
            return students.Documents;
        }
        public IEnumerable<Student> SearchMatchAll(string name)
        {
            var students = client.Search<Student>(s => s.Query(q => q.MatchAll()));
            return students.Documents;
        }
        public IEnumerable<Student> SearchMatchPhraseQuery(string name)
        {
            var students = client.Search<Student>(s => s.Query(q => q.MatchPhrase(m => m.Field(f => f.Content).Query(name))));
            return students.Documents;
        }
        public IEnumerable<Student> SearchTermQuery(string name)
        {
            var students = client.Search<Student>(s => s.Query(q => q.Term(m => m.Content,name)));
            return students.Documents;
        }
        public IndexResponse CreateStudent(int id, string name)
        {
            var students = client.IndexDocument<Student>(new Student() { StudentId = id, Content = name });
            return students;
        }
        public async Task<DeleteByQueryResponse> DeleteAllStudents()
        {
            return await client.DeleteByQueryAsync<Student>(del => del.Query(q => q.QueryString(qs => qs.Query("*"))));
        }

    }
    public class Student
    {
        public int StudentId { get; set; }
        public string Content { get; set; }
    }
}
