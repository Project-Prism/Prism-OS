<hr/>

# Glyphs

Font glyphs are cached in the '[Glyph](https://github.com/Project-Prism/Prism-OS/tree/main/PrismGraphics/Font/Glyph.cs)' class, this allows fonts to be rendered very fast.

## How it works

When a glyph is cached, every pixel with an offset X and Y value is stored into the class, which allows it to directly render only the required pixels. A color can be applied even after caching because each pixel is not color dependant.

<hr/>