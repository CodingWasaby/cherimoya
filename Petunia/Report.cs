using Mathy.Planning;
using Roselle;
using Roselle.Export;
using System.IO;

namespace Petunia
{
    public class Report
    {
        private Document document;

        public Report(Document document)
        {
            this.document = document;
        }


        public void SaveAsWordDocument(string filePath)
        {
            CreateDocumentWriter().Write(filePath);
        }

        public void SaveAsWordDocument(Stream stream)
        {
            CreateDocumentWriter().Write(stream);
        }

        private DocumentWriter CreateDocumentWriter()
        {
            DocumentWriter writer = new DocumentWriter();
            writer.Document = document;
            writer.Exporter = new WordDocumentExporter();
            return writer;
        }


        public static Report FromEvaluationContext(EvaluationContext ec)
        {
            return new ReportBuilder().Build(ec);
        }
    }
}
