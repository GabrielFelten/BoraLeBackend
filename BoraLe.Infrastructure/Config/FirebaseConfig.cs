using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;

namespace BoraLe.Infrastructure.Config
{
    public class FirebaseConfig
    {
        private readonly FirestoreDb _firestoreDb;

        public FirebaseConfig(IConfiguration configuration)
        {
            string projectId = configuration["firebaseConfig:projectId"]
                               ?? throw new Exception("Firebase ProjectId não configurado");

            string credentialsPath = configuration["firebaseConfig:credentialsPath"]
                                     ?? throw new Exception("Caminho do JSON de credenciais não configurado");

            var credential = GoogleCredential.FromFile(credentialsPath);
            _firestoreDb = new FirestoreDbBuilder
            {
                ProjectId = projectId,
                Credential = credential
            }.Build();
        }

        public FirestoreDb GetFirestoreDb() => _firestoreDb;
    }
}
