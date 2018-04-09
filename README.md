# Broadcast Email
Ele envia uma mensagem (corpo do body.html) para uma lista de e-mails em Json (emails.json)

# Como utilizar ?
É simples basta preencher as variaveis "FromEmail", "FromPassword", "SmtpHost", "SmtpPort" e "EmailTitle". Para adicionar ou remover e-mails, basta abrir o arquivo emails.json e adicionar os e-mails no formato abaixo
```json
{
  "emails": [
    {
      "nome": "Nome1",
      "email": "email1@email.com.br"
    },
    {
      "nome": "Nome2",
      "email": "email2@email.com.br"
    }
  ]
}
```
Para alterar a mensagem enviada basta alterar o arquivo body.html

# Tags do html
Caso no html contenha "%nome%" escrito ele substituira pelo nome indicado no json.

# Dll's utilizadas
- Newtonsoft.Json (Para manipular o JSON de retorno do MercadoLivre) : https://www.nuget.org/packages/newtonsoft.json/

