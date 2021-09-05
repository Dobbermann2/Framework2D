#version 460

out vec4 outputColor;

in vec2 texCoord;
in float texSlot;
uniform sampler2D textures[10];

void main()
{
    outputColor = texture(textures[int(texSlot)], texCoord);
}