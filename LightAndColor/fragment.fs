#version 330 core

uniform vec3 lightColor;
uniform vec3 thisColor;
in vec3 Normal;
in vec2 TexCoords;
in vec3 FragPos; 
out vec4 color;
struct Material{
    sampler2D specular;
    float shininess;
    sampler2D diffuse;
    sampler2D emission;
};
struct Light {
    vec3 position;
    vec3 direction;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    float constant;
    float linear;
    float quadratic;
};

uniform Light light;
uniform Material material;
void main()
{
    float distance = length(light.position -  FragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance + 
                light.quadratic * (distance * distance));
    vec3 ambient = texture(material.diffuse, TexCoords).rgb;
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(FragPos - light.position);
    
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse =diff*vec3(texture(material.diffuse, TexCoords)) ;

    vec3 viewDir = normalize(-FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 temp = vec3(texture(material.specular,TexCoords));
    vec3 specular = temp * spec;
    ambient *= attenuation;
    diffuse *= attenuation;
    specular *=attenuation;
    color = vec4((light.ambient*ambient + light.diffuse*diffuse +light.specular*specular ),1.0f); 
}