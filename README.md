# EasyWatermark

方便地为图片添加水印的库。

A library to add watermarks to pictures.

# Build

On Windows:
```powershell
dotnet bulid -c Release
```

# How to use

引用"EasyWatermark.dll"后添下列代码即可。

You should reference "EasyWatermark.dll" to your project and add the following code:
```C#
using EasyWatermark;
```

# Example

给target.jpg的右下角添加一行字，内容为“@测试用水印”，字体为“思源宋体”，字体大小15像素，蓝色，透明度255，处理后的图片保存为“result.jpg”。

The watermark content is “@测试水印”, the font is “思源宋体”, the size is 15 pixels, the color is blue, and the alpha is 255.

```C#
Image watermark = EWatermark.GetWatermark("@测试用水印", 15, new FontFamily("思源宋体"), Color.Blue, 255);

Image result = EWatermark.AddWatermark("target.jpg", watermark);

result.Save("result.jpg", ImageFormat.Jpeg);
```

以watermark.jpg为水印图片为target.jpg打上水印。

Use “watermark.jpg” as a watermark picture to watermark “target.jpg”.


```C#
Image result = EWatermark.AddWatermark("target.jpg", "watermark.jpg");

result.Save("result.jpg", ImageFormat.Jpeg);
```