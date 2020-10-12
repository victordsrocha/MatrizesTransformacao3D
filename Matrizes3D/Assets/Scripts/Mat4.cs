/*
 * Fiz somente Matriz4x4 pois engloba o caso 2D
 */

using UnityEngine;

public class Mat4
{
    private float[,] _m;

    public Mat4()
    {
        Identidade();
    }

    public Vector3 Transformar(Vector3 v)
    {
        Vector3 u = new Vector3(
            _m[0, 0] * v.x + _m[0, 1] * v.y + _m[0, 2] * v.z + _m[0, 3],
            _m[1, 0] * v.x + _m[1, 1] * v.y + _m[1, 2] * v.z + _m[1, 3],
            _m[2, 0] * v.x + _m[2, 1] * v.y + _m[2, 2] * v.z + _m[2, 3]
        );

        var uw = _m[3, 0] * v.x + _m[3, 1] * v.y + _m[3, 2] * v.z + _m[3, 3];
        if (uw != 0)
        {
            u.x /= uw;
            u.y /= uw;
            u.z /= uw;
        }

        return u;
    }


    public void Translacao(float dx, float dy, float dz)
    {
        _m = new float[,]
        {
            {1, 0, 0, dx},
            {0, 1, 0, dy},
            {0, 0, 1, dz},
            {0, 0, 0, 1}
        };
    }

    public void Escala(float sx, float sy, float sz)
    {
        _m = new float[,]
        {
            {sx, 0, 0, 0},
            {0, sy, 0, 0},
            {0, 0, sz, 0},
            {0, 0, 0, 1}
        };
    }

    public void RotacaoEixoX(float theta)
    {
        _m = new float[,]
        {
            {1, 0, 0, 0},
            {0, Mathf.Cos(theta), -Mathf.Sin(theta), 0},
            {0, Mathf.Sin(theta), Mathf.Cos(theta), 0},
            {0, 0, 0, 1}
        };
    }

    public void RotacaoEixoY(float theta)
    {
        _m = new float[,]
        {
            {Mathf.Cos(theta), 0, Mathf.Sin(theta), 0},
            {0, 1, 0, 0},
            {-Mathf.Sin(theta), 0, Mathf.Cos(theta), 0},
            {0, 0, 0, 1}
        };
    }

    public void RotacaoEixoZ(float theta)
    {
        _m = new float[,]
        {
            {Mathf.Cos(theta), Mathf.Sin(theta), 0, 0},
            {-Mathf.Sin(theta), Mathf.Cos(theta), 0, 0},
            {0, 0, 1, 0},
            {0, 0, 0, 1}
        };
    }

    public void RotacaoEixoArbitrario(float theta, Vector3 u)
    {
        // Substituição de u pelo versor de u
        var magnitude = Mathf.Sqrt(u.x * u.x + u.y * u.y + u.z * u.z);
        u.x /= magnitude;
        u.y /= magnitude;
        u.z /= magnitude;

        var cos = Mathf.Cos(theta);
        var sen = Mathf.Sin(theta);
        var x = u.x;
        var y = u.y;
        var z = u.z;

        _m = new float[,]
        {
            {
                x * x * (1 - cos) + cos,
                x * y * (1 - cos) - z * sen,
                x * z * (1 - cos) + y * sen,
                0
            },

            {
                x * y * (1 - cos) + z * sen,
                y * y * (1 - cos) + cos,
                y * z * (1 - cos) - x * sen,
                0
            },

            {
                x * z * (1 - cos) - y * sen,
                y * z * (1 - cos) + x * sen,
                z * z * (1 - cos) + cos,
                0
            },

            {
                0,
                0,
                0,
                1
            }
        };
    }

    public void TRS(float dx = 0, float dy = 0, float dz = 0,
        float theta = 0, Vector3 u = new Vector3(),
        float sx = 1, float sy = 1, float sz = 1)
    {
        var t = new Mat4();
        t.Translacao(dx, dy, dz);

        var r = new Mat4();
        r.RotacaoEixoArbitrario(theta, u);

        var s = new Mat4();
        s.Escala(sx, sy, sz);

        this._m = MultiplicarMatriz(this._m, s._m);
        this._m = MultiplicarMatriz(this._m, r._m);
        this._m = MultiplicarMatriz(this._m, t._m);
    }

    public void TRSinversa(float dx = 0, float dy = 0,
        float dz = 0, float theta = 0, Vector3 u = new Vector3(),
        float sx = 1, float sy = 1, float sz = 1)
    {
        var t = new Mat4();
        t.Translacao(dx, dy, dz);
        t._m = InverterMatriz(t._m);

        var r = new Mat4();
        r.RotacaoEixoArbitrario(theta, u);
        r._m = InverterMatriz(r._m);

        var s = new Mat4();
        s.Escala(sx, sy, sz);
        s._m = InverterMatriz(s._m);

        this._m = MultiplicarMatriz(this._m, s._m);
        this._m = MultiplicarMatriz(this._m, r._m);
        this._m = MultiplicarMatriz(this._m, t._m);
    }

    private void Identidade()
    {
        _m = new float[,]
        {
            {1, 0, 0, 0},
            {0, 1, 0, 0},
            {0, 0, 1, 0},
            {0, 0, 0, 1}
        };
    }

    private float[,] InverterMatriz(float[,] matriz)
    {
        Matrix4x4 m4 = Matrix4x4.zero;

        int c = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                m4[c] = matriz[i, j];
                c++;
            }
        }

        m4 = m4.inverse;

        c = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                matriz[i, j] = m4[c];
                c++;
            }
        }

        return matriz;
    }

    private float[,] MultiplicarMatriz(float[,] matriz1, float[,] matriz2)
    {
        float[,] mult = new float[4, 4];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    mult[i, j] += matriz1[i, k] * matriz2[k, j];
                }
            }
        }

        return mult;
    }
}