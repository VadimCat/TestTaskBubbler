using Unity.AI.Navigation;
using UnityEngine;

public class MeshBubblerView : MonoBehaviour
{
    [SerializeField] private ComputeShader _shader;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private NavMeshSurface navMesh;
    [SerializeField] private Mesh baseMesh;
    
    private ComputeBuffer vertexBuffer;
    private ComputeBuffer displacedVertexBuffer;

    const string MaxHeight = "maxHeight";
    private const string SaveMesh = "saveMesh";
    private const string BubblePoint = "bubblePoint";
    private const string KernelName = "CSMain";
    private const string VertexBufferName = "_VertexBuffer";
    private const string DisplacedVertexBufferName = "_DisplacedVertexBuffer";
    private const string MaxRadius = "maxRadius";
    private const string BubbleDirection = "bubbleDirection";

    private uint _x;
    private int _kernel;
    private int _threadGroups;

    private void Awake()
    {
        navMesh.BuildNavMesh();
    }

    public void InitComputeShader(float radius, float height)
    {
        vertexBuffer = new ComputeBuffer(_meshFilter.mesh.vertexCount, sizeof(float) * 3);
        displacedVertexBuffer = new ComputeBuffer(_meshFilter.mesh.vertexCount, sizeof(float) * 3);

        _kernel = _shader.FindKernel(KernelName);

        _shader.SetBuffer(_kernel, VertexBufferName, vertexBuffer);
        _shader.SetBuffer(_kernel, DisplacedVertexBufferName, displacedVertexBuffer);

        vertexBuffer.SetData(_meshFilter.mesh.vertices);
        _shader.SetFloat(MaxHeight, height);
        _shader.SetFloat(MaxRadius, radius);
        _shader.SetVector(BubbleDirection, Vector3.up);

        _shader.GetKernelThreadGroupSizes(_kernel, out _x, out _, out _);

        _threadGroups = (int)(_meshFilter.mesh.vertices.Length / _x);

        if (_threadGroups * _x < _meshFilter.mesh.vertices.Length)
        {
            _threadGroups++;
        }
    }

    public void Reset()
    {
        vertexBuffer.SetData(baseMesh.vertices);
        displacedVertexBuffer.SetData(baseMesh.vertices);
        
        _meshFilter.mesh.SetVertices(baseMesh.vertices);
        _meshFilter.mesh.RecalculateNormals();
        navMesh.BuildNavMesh();
    }

    public void MoveBubble(Vector3 point)
    {
        _shader.SetVector(BubblePoint, point);
        _shader.Dispatch(_kernel, _threadGroups, 1, 1);

        var array = new Vector3[_meshFilter.mesh.vertices.Length];

        displacedVertexBuffer.GetData(array);
        _meshFilter.mesh.SetVertices(array);
        _meshFilter.mesh.RecalculateNormals();
    }

    public void AddSavedBubble(Vector3 point)
    {
        _shader.SetBool(SaveMesh, true);
        MoveBubble(point);
        _shader.SetBool(SaveMesh, false);
        navMesh.BuildNavMesh();
    }

    private void OnDestroy()
    {
        vertexBuffer.Dispose();
        displacedVertexBuffer.Dispose();
    }
}