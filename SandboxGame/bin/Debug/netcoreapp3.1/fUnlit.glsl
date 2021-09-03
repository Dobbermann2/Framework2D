#version 460

out vec4 outputColor;

in vec2 texCoord;
flat in int texSlot;
uniform sampler2D textures[5];

void main()
{
    outputColor = texture(textures[texSlot], texCoord);
}