#version 330 core

uniform vec3 lightColor;
uniform vec3 thisColor;
in vec3 Normal;
in vec2 TexCoords;
in vec3 FragPos; 
uniform vec3 lightPos;
out vec4 color;
struct Material{
    sampler2D specular;
    float shininess;
    sampler2D diffuse;
    sampler2D emission;
};
struct Light {
    vec3 position;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform Light light;
uniform Material material;
void main()
{
    vec3 ambient = texture(material.diffuse, TexCoords).rgb;

    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse =diff*vec3(texture(material.diffuse, TexCoords)) ;

    vec3 viewDir = normalize(-FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 temp = vec3(texture(material.specular,TexCoords));
    //vec3 specular = (vec3(1.0) - temp)*  spec; 
    vec3 specular = temp * spec;
    vec3 emission = texture(material.emission,TexCoords).rgb;
    color = vec4((light.ambient*ambient + light.diffuse*diffuse +light.specular*specular + emission )  ,1.0f); 
}